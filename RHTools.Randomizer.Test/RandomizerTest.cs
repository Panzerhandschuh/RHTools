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
				new MaxSpanRule(2, 0),
				new DisableCenterPanelOverlapRule()
			};

			var settings = GetSettings(rules);
			var randomizer = new RhcRandomizer(settings);
			randomizer.Randomize();
		}

		private RandomizerSettings GetSettings(List<Rule> rules)
		{
			var settings = new RandomizerSettings();

			settings.rhPath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\733990c7-17c2-41b7-bdad-3b67cb319bd2.rhc";
			settings.panelConfig = RandomizerTestUtil.config9Panel;
			settings.random = new Random();
			settings.rules = rules;

			return settings;
		}
	}
}
