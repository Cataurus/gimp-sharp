// The PhotoshopActions plug-in
// Copyright (C) 2006-2016 Maurits Rijk
//
// Action.cs
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
using System.Linq;

namespace Gimp.PhotoshopActions
{
  public class Action : IExecutable
  {
    readonly List<ActionEvent> _set = new List<ActionEvent>();

    bool _enabled = true;

    public byte ShiftKey {private get; set;}
    public byte CommandKey {private get; set;}
    public int ColorIndex {private get; set;}
    public byte Expanded {get; set;}
    public int NrOfChildren {get; set;}
    public string Name {get; set;}

    public bool IsExecutable => ActionEvents == NrOfChildren &&
      _set.TrueForAll(e => e.IsExecutable);

    public bool IsEnabled
    {
      get {return _enabled;}
      set 
	{
	  _enabled = value;
	  _set.ForEach(e => {e.IsEnabled = value;});
	}
    }

    public int ActionEvents => _set.Count;

    public int ExecutableActionEvents => _set.Where(e => e.IsExecutable).Count();

    public void Add(ActionEvent actionEvent)
    {
      _set.Add(actionEvent);
    }

    public void Execute()
    {
      for (int i = 0; i < ActionEvents; i++)
	{
	  var actionEvent = _set[i];

	  // Check if we need to save the layer because of a following
	  // Fade event.

	  if (i < ActionEvents - 1)
	    {
	      var next = _set[i + 1];
	      if (next is FadeEvent)
		{
		  if (ActionEvent.SelectedLayer != null)
		    {
		      Console.WriteLine("bpp: " + 
					ActionEvent.SelectedLayer.Bpp);
		    }
		  var layer = new Layer(ActionEvent.SelectedLayer);
		  ActionEvent.ActiveImage.InsertLayer(layer, 0);
		  ActionEvent.SelectedLayer = layer;
		}
	    }
	  
	  if (!actionEvent.Execute())
	    {
	      break;
	    }
	}
    }

    public void Execute(int n)
    {
      _set[n].Execute();
    }

    public IEnumerator<ActionEvent> GetEnumerator() => _set.GetEnumerator();
  }
}
