// GIMP# - A C# wrapper around the GIMP Library
// Copyright (C) 2004-2006 Maurits Rijk
//
// TestLayer.cs
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the
// Free Software Foundation, Inc., 59 Temple Place - Suite 330,
// Boston, MA 02111-1307, USA.
//

using System;

using NUnit.Framework;

namespace Gimp
{
  [TestFixture]
  public class TestLayer
  {
    int _width = 64;
    int _height = 128;
    Image _image;

    [SetUp]
    public void Init()
    {
      _image = new Image(_width, _height, ImageBaseType.RGB);
    }

    [TearDown]
    public void Exit()
    {
      _image.Delete();
    }

    [Test]
    public void New()
    {
      Layer layer = new Layer(_image, "test", _width, _height,
			      ImageType.RGB, 100, 
			      LayerModeEffects.NORMAL);
      _image.AddLayer(layer, 0);
      Assert.AreEqual(1, _image.Layers.Count);
    }

    [Test]
    public void Resize()
    {
      Layer layer = new Layer(_image, "test", _width, _height,
			      ImageType.RGB, 100, 
			      LayerModeEffects.NORMAL);
      _image.AddLayer(layer, 0);
      layer.Resize(2 * _width, 2 * _height, 0, 0);
      Assert.AreEqual(2 * _width, layer.Width);
      Assert.AreEqual(2 * _height, layer.Height);
    }

    [Test]
    public void Translate()
    {
      Layer layer = new Layer(_image, "test", _width, _height,
			      ImageType.RGB, 100, 
			      LayerModeEffects.NORMAL);
      _image.AddLayer(layer, 0);
      layer.Translate(-10, 10);

      int off_x, off_y;
      layer.Offsets(out off_x, out off_y);
      Assert.AreEqual(-10, off_x);
      Assert.AreEqual(10, off_y);
    }
  }
}
