// The Pointillize plug-in
// Copyright (C) 2006-2018 Maurits Rijk
//
// ColorCoordinateSet.cs
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
//

using System;
using System.Collections;
using System.Collections.Generic;

namespace Gimp.Pointillize
{
  public class ColorCoordinateSet : CoordinateList<int>
  {
    readonly int _cellSize2;
    readonly int _width;
    readonly int _height;
    readonly int _matrixRows;
    readonly int _matrixColumns;

    readonly Pixel _backgroundColor;

    readonly List<ColorCoordinate>[,] _matrix;

    public ColorCoordinateSet(Drawable drawable, int cellSize)
    {
      _cellSize2 = cellSize * cellSize;

      _backgroundColor = new Pixel(Context.Background.Bytes);

      var pf = new PixelFetcher(drawable, false);

      (_width, _height) = drawable.Dimensions;
      
      int nrOfCells = (int) (2.5 * _width * _height / _cellSize2);

      _matrixColumns = (int) Math.Sqrt(nrOfCells * _width / 8.0 / _height);
      _matrixRows = _matrixColumns * _height / _width;

      _matrixColumns = Math.Max(_matrixColumns, 1);
      _matrixRows = Math.Max(_matrixRows, 1);
      
      _matrix = new List<ColorCoordinate>[_matrixRows, _matrixColumns];

      foreach (var c in new RandomCoordinateGenerator(_width - 1, _height - 1, 
						      nrOfCells))
	{
	  var (x, y) = c;

	  var color = pf.GetPixel(c);
	  color.AddNoise(5);

	  var coordinate = new ColorCoordinate(c, color);
	  Add(coordinate);

	  int row = y * _matrixRows / _height;
	  int col = x * _matrixColumns / _width;
	  
	  Add(row, col, coordinate);

	  int top = row * _height / _matrixRows;
	  int left = col * _width / _matrixColumns;
	  int bottom = (row + 1) * _height / _matrixRows;
	  int right = (col + 1) * _width / _matrixColumns;
	  
	  Intersects(left, top, col - 1, row - 1, coordinate);
	  Intersects(x, top, col, row - 1, coordinate);
	  Intersects(right, top, col + 1, row - 1, coordinate);
	  Intersects(left, y, col - 1, row, coordinate);
	  Intersects(right, y, col + 1, row, coordinate);
	  Intersects(left, bottom, col - 1, row + 1, coordinate);
	  Intersects(x, bottom, col, row + 1, coordinate);
	  Intersects(right, bottom, col + 1, row + 1, coordinate);
	}

      pf.Dispose();
    }

    void Intersects(int x, int y, int col, int row, ColorCoordinate coordinate)
    {
      if (col < 0 || col >= _matrixColumns || row < 0 || row >= _matrixRows)
	{
	  return;
	}
      
      if (coordinate.Distance(x, y) < _cellSize2 / 4)
	{
	  Add(row, col, coordinate);
	}
    }

    void Add(int row, int col, ColorCoordinate coordinate)
    {
      _matrix[row, col] = _matrix[row, col] ?? new List<ColorCoordinate>();
      _matrix[row, col].Add(coordinate);
    }

    public Pixel GetColor(IntCoordinate c)
    {
      int row = c.Y * _matrixRows / _height;
      int col = c.X * _matrixColumns / _width;
      
      var list = _matrix[row, col];

      return (list == null) ? _backgroundColor : GetNearestColor(list, c);
    }

    Pixel GetNearestColor(List<ColorCoordinate> list, IntCoordinate c)
    {
      int distance = int.MaxValue;
      ColorCoordinate closest = null;

      list.ForEach(coordinate => 
	{
	  int d = coordinate.Distance(c);
	  if (d < distance)
	    {
	      distance = d;
	      closest = coordinate;
	    }
	});

      return distance < _cellSize2 / 4 ? closest.Color : _backgroundColor;
    }
  }
}
