// The PhotoshopActions plug-in
// Copyright (C) 2006-2016 Maurits Rijk
//
// TransformEvent.cs
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

namespace Gimp.PhotoshopActions
{
  public class TransformEvent : ActionEvent
  {
    [Parameter("null")]
    ReferenceParameter _obj;

    readonly bool _executable;

    public TransformEvent()
    {
    }

    public TransformEvent(ActionEvent srcEvent) : base(srcEvent)
    {
      _executable = true;
    }

    public override bool IsExecutable => _executable;

    override public ActionEvent Parse(ActionParser parser)
    {
      base.Parse(parser);

      if (_obj != null)
	{
	  if (_obj.Set[0] is EnmrType)
	    {
	      var enmr = _obj.Set[0] as EnmrType;
	      switch (enmr.Key)
		{
		case "Lyr":
		  return new TransformLayerEvent(this);
		default:
		  Console.WriteLine("Transform-2: unknown key " + enmr.Key);
		  break;
		}
	    }
	  else if (_obj.Set[0] is PropertyType)
	    {
	      var property = _obj.Set[0] as PropertyType;
	      switch (property.Key)
		{
		case "fsel":
		  return new TransformSelectionEvent(this);
		default:
		  Console.WriteLine("Transform-4: unknown key " + 
				    property.Key);
		  break;
		}
	    }
	  else
	    {
	      Console.WriteLine("Transform-3: " + _obj.Set[0]);
	    }
	}
      else
	{
	  Console.WriteLine("Transform-1");
	}

      return this;
    }
  }
}
