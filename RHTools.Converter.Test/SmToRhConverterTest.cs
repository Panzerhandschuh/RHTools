using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RHTools.Converter.Test
{
	[TestClass]
	public class SmToRhConverterTest
	{
		const string smPath = @"D:\Program Files (x86)\Stepmania 3.95\Songs\9V Recharged\120 Seconds";
		const string rhPath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData";

		[TestMethod]
		public void ConvertSmToRh()
		{
			SmToRhConverter converter = new SmToRhConverter();
			converter.ConvertSmFile(smPath, rhPath);
		}
	}
}
