// The Slice Tool plug-in
// Copyright (C) 2004-2016 Maurits Rijk
//
// SaveSettingsButton.cs
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

using Mono.Unix;

using Gtk;

namespace Gimp.SliceTool
{
  public class SaveSettingsButton : Button
  {
    public SaveSettingsButton(Dialog parent, SliceData sliceData) : 
      base(Catalog.GetString("Save Settings..."))
    {
      Clicked += delegate
	{
	  var fc = new FileChooserDialog(Catalog.GetString("Save Settings"), 
					 parent, FileChooserAction.Save,
					 "Cancel", ResponseType.Cancel,
					 "Save", ResponseType.Accept);

	  if (fc.Run() == (int) ResponseType.Accept)
	  {
	    sliceData.SaveSettings(fc.Filename);
	  }
	  fc.Destroy();
	};
    }
  }
}
