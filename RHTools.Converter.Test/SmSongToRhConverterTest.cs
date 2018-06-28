using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RHTools.Converter.Test
{
	[TestClass]
	public class SmSongToRhConverterTest
	{
		private const string smPath = @"D:\Program Files (x86)\OpenITG\Songs\Tachyon Epsilon\Smiting Down the Weak - [Zeph + Zaia]";
		private const string rhPath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData";

		[TestMethod]
		public void ConvertSmSongToRh()
		{
			var converter = new SmSongToRhConverter(smPath, rhPath);
			converter.Convert();
		}
	}
}
