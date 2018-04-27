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
			string[] files = Directory.GetFiles(gameDir, "*.rhg");
			List<RhgFile> rhgFiles = new List<RhgFile>();
			foreach (string file in files)
				rhgFiles.Add(ReadRhgFile(file));
		}

		[TestMethod]
		public void WriteRhgFileBytesEqualsOriginalRhgFileBytes()
		{
			string[] files = Directory.GetFiles(gameDir, "*.rhg");
			foreach (string file in files)
			{
				RhgFile rhgFile = ReadRhgFile(file);

				byte[] writeBytes = rhgFile.SerializeToBytes();
				byte[] originalBytes = File.ReadAllBytes(file);

				Assert.IsTrue(Enumerable.SequenceEqual(originalBytes, writeBytes));
			}
		}

		private RhgFile ReadRhgFile(string path)
		{
			using (Stream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (BinaryReader reader = new BinaryReader(stream))
			{
				return RhgFile.Deserialize(reader);
			}
		}
	}
}
