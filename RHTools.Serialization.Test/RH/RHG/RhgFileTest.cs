using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.Test.RH
{
	[TestClass]
	public class RhgFileTest
	{
		const string gameDir = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData";

		[TestMethod]
		public void ReadRhgFiles()
		{
			var files = Directory.GetFiles(gameDir, "*.rhg");
			var rhgFiles = new List<RhgFile>();
			foreach (var file in files)
				rhgFiles.Add(ReadRhgFile(file));
		}

		[TestMethod]
		public void WriteRhgFileBytesEqualsOriginalRhgFileBytes()
		{
			var files = Directory.GetFiles(gameDir, "*.rhg");
			foreach (var file in files)
			{
				var rhgFile = ReadRhgFile(file);

				var writeBytes = rhgFile.SerializeToBytes();
				var originalBytes = File.ReadAllBytes(file);

				Assert.IsTrue(Enumerable.SequenceEqual(originalBytes, writeBytes));
			}
		}

		private RhgFile ReadRhgFile(string path)
		{
			return IBinarySerializableExtensions.Deserialize(path, RhgFile.Deserialize);
		}
	}
}
