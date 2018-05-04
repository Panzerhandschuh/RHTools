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
	public class RhcFileTest
	{
		const string gameDir = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData";

		[TestMethod]
		public void ReadRhcFile()
		{
			RhcFile file = ReadRhcFile(@"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\833abd96-38d3-4b6e-bcf7-d19249e328c6.rhc");
		}

		[TestMethod]
		public void ReadRhcFiles()
		{
			string[] files = Directory.GetFiles(gameDir, "*.rhc");
			List<RhcFile> rhcFiles = new List<RhcFile>();
			foreach (string file in files)
				rhcFiles.Add(ReadRhcFile(file));
		}

		[TestMethod]
		public void WriteRhcFileBytesEqualsOriginalRhcFileBytes()
		{
			string[] files = Directory.GetFiles(gameDir, "*.rhc");
			foreach (string file in files)
			{
				RhcFile rhcFile = ReadRhcFile(file);

				byte[] writeBytes = rhcFile.SerializeToBytes();
				byte[] originalBytes = File.ReadAllBytes(file);

				Assert.IsTrue(Enumerable.SequenceEqual(originalBytes, writeBytes));
			}
		}

		private RhcFile ReadRhcFile(string path)
		{
			return IBinarySerializableExtensions.Deserialize(path, RhcFile.Deserialize);
		}
	}
}
