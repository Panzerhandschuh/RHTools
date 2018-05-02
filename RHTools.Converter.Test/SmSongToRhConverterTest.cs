using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RHTools.Converter.Test
{
	[TestClass]
	public class SmSongToRhConverterTest
	{
		const string smPath = @"D:\Program Files (x86)\Stepmania 3.95\Songs\9V Recharged\120 Seconds";
		const string rhPath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData";

		[TestMethod]
		public void ConvertSmSongToRh()
		{
			SmSongToRhConverter converter = new SmSongToRhConverter(smPath, rhPath);
			converter.Convert();
		}
	}
}
