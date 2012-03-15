// GIMP# - A C# wrapper around the GIMP Library
// Copyright (C) 2004-2012 Maurits Rijk
//
// GimpParamSet.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Gimp
{
  public class GimpParamSet : IEnumerable<GimpParam>
  {
    List<GimpParam> _set = new List<GimpParam>();

    public GimpParamSet()
    {
    }

    public GimpParamSet(IntPtr ptr, int n)
    {
      int size = GimpParam.Size;

      for (int i = 0; i < n; i++)
	{
	  Add(new GimpParam(ptr));
	  ptr = (IntPtr)((int)ptr + size);
	}
    }

    public void Add(GimpParam param)
    {
      _set.Add(param);
    }

    public IEnumerator<GimpParam> GetEnumerator()
    {
      return _set.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public int Count
    {
      get {return _set.Count;}
    }

    public IntPtr ToStructArray()
    {
      int size = GimpParam.Size;
 
      IntPtr returnVals = Marshal.AllocCoTaskMem(Count * size);
      IntPtr paramPtr = returnVals;

      foreach (GimpParam param in _set)
	{
	  param.Fill(paramPtr);
	  paramPtr = (IntPtr)((int) paramPtr + size);
	}

      return returnVals;
    }
  }
}