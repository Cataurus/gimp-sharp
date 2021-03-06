// The PhotoshopActions plug-in
// Copyright (C) 2006-2018 Maurits Rijk
//
// SelectEllipseEvent.cs
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

using System.Collections;

namespace Gimp.PhotoshopActions
{
  public class SelectEllipseEvent : SelectionEvent
  {
    ObjcParameter _objc;

    public SelectEllipseEvent(SelectionEvent srcEvent, ObjcParameter objc) : 
      base(srcEvent)
    {
      _objc = objc;
    }

    protected override IEnumerable ListParameters()
    {
      yield return "To: ellipse";
    }

    override public bool Execute()
    {
      GetBounds(_objc, out double x, out double y, out double width, out double height);
      EllipseSelectTool tool = new EllipseSelectTool(ActiveImage);
      tool.Select(x, y ,width, height, ChannelOps.Replace, 
		  true, false, 0);
      RememberCurrentSelection();

      return true;
    }
  }
}
