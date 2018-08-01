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
		public bool disableJumps;
		public bool disableMines;
	}
}
