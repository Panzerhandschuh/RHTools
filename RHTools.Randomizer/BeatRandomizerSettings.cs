using RHTools.Randomizer.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer
{
	public class BeatRandomizerSettings
	{
		public bool[,] panelConfig;
		public Random random;
		public List<Rule> noteRules;
		public List<Rule> mineRules;

		public BeatRandomizerSettings() { }

		public BeatRandomizerSettings(bool[,] panelConfig, Random random, List<Rule> noteRules, List<Rule> mineRules)
		{
			this.panelConfig = panelConfig;
			this.random = random;
			this.noteRules = noteRules;
			this.mineRules = mineRules;
		}
	}
}
