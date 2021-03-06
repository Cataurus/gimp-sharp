// The Splitter plug-in
// Copyright (C) 2004-2016 Maurits Rijk
//
// AdvancedDialog.cs
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

namespace Gimp.Splitter
{
  public class AdvancedDialog : GimpDialog
  {
    public AdvancedDialog(Variable<UInt32> seed, Variable<bool> randomSeed) : 
      base(_("Advanced Settings"), _("Splitter"))
    {
      var table = new GimpTable(1, 2, false) {
	BorderWidth = 12, ColumnSpacing = 6, RowSpacing = 6};
      VBox.PackStart(table, true, true, 0);

      var random = new RandomSeed(seed, randomSeed);
      
      table.AttachAligned(0, 0, _("Random _Seed:"), 0.0, 0.5, random, 2, true);
    }
  }
}
