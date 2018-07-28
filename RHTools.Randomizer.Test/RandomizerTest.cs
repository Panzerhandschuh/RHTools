using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.Randomizer.Rules;
using RHTools.Randomizer.Test.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Test
{
	[TestClass]
	public class RandomizerTest
	{
		[TestMethod]
		public void RandomizeRhc()
		{
			var settings = new RandomizerSettings();
			settings.rhPath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\00f1652d-a278-4f61-a63b-7a58cd8c66c0.rhc";
			settings.panelConfig = RandomizerTestUtil.config9Panel;
			settings.random = new Random();
			settings.rules = new List<Rule>();

			var randomizer = new RhcRandomizer(settings);
			randomizer.Randomize();
		}
	}
}
