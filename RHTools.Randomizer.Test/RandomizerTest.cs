using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.Randomizer.Rules;
using RHTools.Randomizer.Test.Utils;
using RHTools.Randomizer.Utils;
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
			var settings = GetSettings(new List<Rule>());
			var randomizer = new RhcRandomizer(settings);
			randomizer.Randomize();
		}

		[TestMethod]
		public void RandomizeWithRules()
		{
			var rules = new List<Rule>()
			{
				new MaxRepetitionsEveryPressRule(0),
				new MaxReptitionsEveryOtherPressRule(1),
				new DisableColumnForFootRule(Foot.Left, 2), // TODO: Need a better rule for automatically detecting the rightmost column in the panel config
				new DisableColumnForFootRule(Foot.Right, 0),
				new MaxStretchDistanceRule(2),
				new MaxCrossoverDistanceRule(0),
				new DisableCenterPanelOverlapRule()
			};

			var settings = GetSettings(rules);
			var randomizer = new RhcRandomizer(settings);
			randomizer.Randomize();
		}

		private RandomizerSettings GetSettings(List<Rule> rules)
		{
			var settings = new RandomizerSettings();

			settings.rhPath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\a8a73cea-8fe5-4807-b242-41ac35db97ce.rhc";
			settings.panelConfig = RandomizerTestUtil.config9Panel;
			settings.random = new Random();
			settings.rules = rules;

			return settings;
		}
	}
}
