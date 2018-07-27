using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.Mixer.Test.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Mixer.Test
{
	[TestClass]
	public class MixerTest
	{
		[TestMethod]
		public void MixRhc()
		{
			var mixer = new Mixer(@"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\00f1652d-a278-4f61-a63b-7a58cd8c66c0.rhc");
			mixer.Mix(MixerTestUtil.config9Panel);
		}
	}
}
