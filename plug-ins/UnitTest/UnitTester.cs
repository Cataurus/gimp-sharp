// The UnitTest plug-in
// Copyright (C) 2004-2012 Maurits Rijk
//
// UnitTester.cs
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
using System.IO;
using NUnit.Core;
using NUnit.Util;

using Gtk;

namespace Gimp.UnitTest
{
  public class UnitTester
  {
    readonly TestDomain _testDomain;
    readonly TestRunner _testRunner;

    public UnitTester()
    {
      _testDomain = new TestDomain();
      _testRunner = _testDomain;
    }

    private static bool MakeTestFromCommandLine(TestDomain testDomain,
						string testDll)
    {
      ServiceManager.Services.AddService(new DomainManager());
      var package = new TestPackage(testDll);
      return testDomain.Load(package);
    }
    
    public void Test(string testDll, Variable<int> performed, 
		     Variable<int> total)
    {
      bool success = false;

      try
	{
	  success = MakeTestFromCommandLine(_testDomain, testDll);
	}
      catch(System.IO.FileNotFoundException)
	{
	  new Message("Failed opening " + testDll);
	  return;
	}

      if (!success)
	{
	  Console.Error.WriteLine("Unable to locate fixture");
	  return;
	}

      total.Value = _testRunner.CountTestCases(TestFilter.Empty);
      
      var collector = new EventCollector(Console.Out, Console.Error, performed);
      var result = _testRunner.Run(collector);
    }
  }
}
