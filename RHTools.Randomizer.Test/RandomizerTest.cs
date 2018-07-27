using Microsoft.VisualStudio.TestTools.UnitTesting;
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
			var randomizer = new Randomizer(@"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\00f1652d-a278-4f61-a63b-7a58cd8c66c0.rhc");
			randomizer.Randomize(RandomizerTestUtil.config9Panel);
		}
	}
}
