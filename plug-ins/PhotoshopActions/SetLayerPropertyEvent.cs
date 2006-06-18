// The PhotoshopActions plug-in
// Copyright (C) 2006 Maurits Rijk
//
// SetLayerPropertyEvent.cs
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
  public class SetLayerPropertyEvent : ActionEvent
  {
    [Parameter("T")]
    ObjcParameter _objc;

    public SetLayerPropertyEvent(ActionEvent srcEvent) : base(srcEvent)
    {
      Parameters.Fill(this);
    }

    override public bool Execute()
    {
      foreach (Parameter parameter in _objc.Parameters)
	{
	  switch (parameter.Name)
	    {
	    case "Md":
	      string mode = (parameter as EnumParameter).Value;
	      switch (mode)
		{
		case "Ovrl":
		  SelectedLayer.Mode = LayerModeEffects.Overlay;
		  break;
		default:
		  Console.WriteLine("Implement set layer mode: " + mode);
		  break;
		}
	      break;
	    case "Nm":
	      SelectedLayer.Name = (parameter as TextParameter).Value;
	      break;
	    case "Opct":
	      SelectedLayer.Opacity = (parameter as DoubleParameter).Value;
	      break;
	    default:
	      break;
	    }
	}
      return true;
    }
  }
}
