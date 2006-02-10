// The PicturePackage plug-in
// Copyright (C) 2004-2006 Maurits Rijk
//
// FileImageProvider.cs
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

namespace Gimp.PicturePackage
{
  public class FileImageProvider : ImageProvider
  {
    Image _image;
    string _filename;
    string _rawFilename;

    public FileImageProvider(string filename)
    {
      _filename = filename;
      _rawFilename = filename;
    }

    public FileImageProvider(string filename, string rawFilename)
    {
      _filename = filename;
      _rawFilename = rawFilename;
    }

    override public Image GetImage()
    {
      if (_image == null)
	{
	  _image = Image.Load(RunMode.NONINTERACTIVE, _filename, _rawFilename);
	}
      return _image;
    }

    override public void Release()
    {
      if (_image != null)
	{
	  _image.Delete();
	  _image = null;
	}
    }
  }
}
