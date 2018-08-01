using RHTools.Randomizer.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer
{
	/// <summary>
	/// Encapsulates the inputs used to generate randomized charts
	/// </summary>
	public class RandomizerSettings : BeatRandomizerSettings
	{
		/// <summary>
		/// RHC, RHS, or RHG file
		/// </summary>
		public string rhPath;
		//public static RhFileType fileType;
	}
}
