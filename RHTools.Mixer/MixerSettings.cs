using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Mixer
{
	public static class MixerSettings
	{
		public static bool[,] panelConfig;
		public static Random random;

		static MixerSettings()
		{
			random = new Random();
		}
	}
}
