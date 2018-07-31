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
		public void RandomizeWithNoRules()
		{
			var settings = GetSettings(new List<Rule>(), new List<Rule>());
			var randomizer = new RhcRandomizer(settings);
			randomizer.Randomize();
		}

		[TestMethod]
		public void RandomizeWithRules()
		{
			var noteRules = new List<Rule>()
			{
				new MaxRepetitionsEveryXNotesRule(0, 1),
				new MaxRepetitionsEveryXNotesRule(1, 2),
				new DisableColumnForFootRule(Foot.Left, 2),
				new DisableColumnForFootRule(Foot.Right, 0),
				new MaxSpanRule(2, 0),
				new DisableCenterPanelOverlapRule()
			};

			var mineRules = new List<Rule>()
			{
				new DisableLastXNotesRule(1)
			};

			var settings = GetSettings(noteRules, mineRules);
			var randomizer = new RhcRandomizer(settings);
			randomizer.Randomize();
		}

		private RandomizerSettings GetSettings(List<Rule> noteRules, List<Rule> mineRules)
		{
			var settings = new RandomizerSettings();

			settings.rhPath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\d1344d30-4b50-457e-b28d-c813eb78bf0a.rhc";
			settings.panelConfig = RandomizerTestUtil.config9Panel;
			settings.random = new Random();
			settings.noteRules = noteRules;
			settings.mineRules = mineRules;

			return settings;
		}
	}
}
