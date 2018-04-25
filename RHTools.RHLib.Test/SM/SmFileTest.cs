using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.RHLib.SM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.Test.SM
{
	[TestClass]
	public class SmFileTest
	{
		const string smFilePath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\Butterfly.sm";

		[TestMethod]
		public void ReadSmFiles()
		{
			Assert.Fail();
		}

		[TestMethod]
		public void ReadSmFile()
		{
			using (Stream stream = File.Open(smFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (StreamReader reader = new StreamReader(stream))
			{
				SmFile file = SmFile.Deserialize(reader);
			}
		}
	}
}
