// The PhotoshopActions plug-in
// Copyright (C) 2006 Maurits Rijk
//
// EventMap.cs
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
using System.Reflection;

namespace Gimp.PhotoshopActions
{
  public class EventMap
  {
    readonly Dictionary<string, string> _map = 
      new Dictionary<string, string>();
    
    readonly Dictionary<string, int> _statistics =
      new Dictionary<string, int>();

    public EventMap()
    {
      _map["addNoise"] = "AddNoiseEvent";
      _map["border"] = "BorderEvent";
      _map["brightnessEvent"] = "BrightnessEvent";
      _map["canvasSize"] = "CanvasSizeEvent";
      _map["clouds"] = "CloudsEvent";
      _map["convertMode"] = "ConvertModeEvent";
      _map["copyEvent"] = "CopyEvent";
      _map["copyToLayer"] = "CopyToLayerEvent";
      _map["delete"] = "DeleteEvent";
      _map["desaturate"] = "DesaturateEvent";
      _map["differenceClouds"] = "DifferenceCloudsEvent";
      _map["duplicate"] = "DuplicateEvent";
      _map["emboss"] = "EmbossEvent";
      _map["exchange"] = "ExchangeEvent";
      _map["facet"] = "FacetEvent";
      _map["feather"] = "FeatherEvent";
      _map["fill"] = "FillEvent";
      _map["findEdges"] = "FindEdgesEvent";
      _map["flattenImage"] = "FlattenImageEvent";
      _map["flip"] = "FlipEvent";
      _map["gaussianBlur"] = "GaussianBlurEvent";
      _map["hide"] = "HideEvent";
      _map["hueSaturation"] = "HueSaturationEvent";
      _map["imageSize"] = "ImageSizeEvent";
      _map["invert"] = "InvertEvent";
      _map["levels"] = "LevelsEvent";
      _map["make"] = "MakeEvent";
      _map["median"] = "MedianEvent";
      _map["mergeLayers"] = "MergeLayersEvent";
      _map["mergeVisible"] = "MergeVisibleEvent";
      _map["move"] = "MoveEvent";
      _map["motionBlur"] = "MotionBlurEvent";
      _map["paste"] = "PasteEvent";
      _map["photocopy"] = "PhotocopyEvent";
      _map["radialBlur"] = "RadialBlurEvent";
      _map["replaceColor"] = "ReplaceColorEvent";
      _map["reset"] = "ResetEvent";
      _map["rotateEventEnum"] = "RotateEvent";
      _map["select"] = "SelectEvent";
      _map["set"] = "SetEvent";
      _map["sharpen"] = "SharpenEvent";
      _map["sharpenMore"] = "SharpenMoreEvent";
      _map["smoothness"] = "SmoothnessEvent";
      _map["stop"] = "StopEvent";
      _map["twirl"] = "TwirlEvent";
      _map["unsharpMask"] = "UnsharpMaskEvent";

      // Pre-6 events
      _map["Clds"] = "CloudsEvent";
    }

    public ActionEvent Lookup(string eventName)
    {
      ActionEvent myEvent;
      string eventType;

      if (_map.TryGetValue(eventName, out eventType))
	{
	  eventType = "Gimp.PhotoshopActions." + eventType;
	  Type type = Assembly.GetEntryAssembly().GetType(eventType);

	  myEvent = (ActionEvent) Activator.CreateInstance(type);
	}
      else
	{
	  Console.WriteLine("Event {0} unsupported", eventName);

	  myEvent = new UnimplementedEvent();

	  int amount;
	  if (!_statistics.TryGetValue(eventName, out amount))
	    {
	      amount = 0;
	    }
	  _statistics[eventName] = ++amount;
	}
      return myEvent;
    }

    public void DumpStatistics()
    {
      foreach (KeyValuePair<string, int> kvp in _statistics)
	{
	  Console.WriteLine("{0} : {1}", kvp.Key, kvp.Value);
	}
    }
  }
}