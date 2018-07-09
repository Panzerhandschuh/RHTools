using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter.Test
{
	[TestClass]
	public class SmSongToRhprojConverterTest
	{
		private const float songOffset = -0.009f;
		private const string smPath = @"D:\Program Files (x86)\OpenITG\Songs\Tachyon Epsilon\++ - [TheBoxX]";
		private const string rhPath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData";

		[TestMethod]
		public void ConvertSmSongToRhproj()
		{
			var converter = new SmSongToRhprojConverter(smPath, rhPath, songOffset);
			converter.Convert();
		}
	}
}
