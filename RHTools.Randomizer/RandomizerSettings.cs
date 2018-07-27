using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer
{
	public static class RandomizerSettings
	{
		public static bool[,] panelConfig;
		public static Random random;

		static RandomizerSettings()
		{
			random = new Random();
		}
	}
}
