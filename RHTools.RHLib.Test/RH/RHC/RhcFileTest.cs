using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.Serialization.RH;
using RHTools.Serialization.Serialization;
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
			RhcFile file = ReadRhcFile(@"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\6ce1a5da-30b7-40a0-8eab-bdfa72a1ec0d.rhc");
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
			using (Stream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (BinaryReader reader = new BinaryReader(stream))
			{
				return RhcFile.Deserialize(reader);
			}
		}
	}
}
