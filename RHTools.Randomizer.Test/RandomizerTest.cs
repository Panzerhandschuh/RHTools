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
				new MaxRepetitionsEveryXNotesRule(0, 1),
				new MaxRepetitionsEveryXNotesRule(1, 2),
				new DisableColumnForFootRule(Foot.Left, 2),
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

			settings.rhPath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\af75efa4-11b7-4be0-a194-fd98b0f7cece.rhc";
			settings.panelConfig = RandomizerTestUtil.config9Panel;
			settings.random = new Random();
			settings.rules = rules;

			return settings;
		}
	}
}
