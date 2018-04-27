using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.Serialization.SM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.Test.SM
{
	[TestClass]
	public class MsdFileTest
	{
		const string msdFilePath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\Butterfly.sm";

		[TestMethod]
		public void ReadMsdFile()
		{
			using (Stream stream = File.Open(msdFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (StreamReader reader = new StreamReader(stream))
			{
				MsdFile file = MsdFile.Deserialize(reader);
			}
		}
	}
}
