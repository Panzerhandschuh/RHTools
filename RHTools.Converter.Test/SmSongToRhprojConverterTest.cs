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
		const string smPath = @"D:\Program Files (x86)\OpenITG\Songs\Tachyon Epsilon\Smiting Down the Weak - [Zeph + Zaia]";
		const string rhPath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData";

		[TestMethod]
		public void ConvertSmSongToRhproj()
		{
			SmSongToRhprojConverter converter = new SmSongToRhprojConverter(smPath, rhPath);
			converter.Convert();
		}
	}
}
