// The PhotoshopActions plug-in
// Copyright (C) 2006 Maurits Rijk
//
// ListParameter.cs
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
using System.Reflection;

namespace Gimp.PhotoshopActions
{
  public class ListParameter : Parameter
  {
    public override void Parse(ActionParser parser)
    {
      int number = parser.ReadInt32();
      Console.WriteLine("\t\tnumber: " + number);
      
      for (int i = 0; i < number; i++)
	{
	  string type = parser.ReadFourByteString();
	  Console.WriteLine("\t\ttype: " + type);
	  if (type == "Objc")
	    {
	      parser.ReadDescriptor();
	    }
	  else
	    {
	      Console.WriteLine("ReadVlLs: type {0} unknown!", type);
	      return;
	    }
	}
    }

    public override void Fill(Object obj, FieldInfo field)
    {
    }
  }
}
