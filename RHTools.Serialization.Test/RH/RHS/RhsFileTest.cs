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
	public class RhsFileTest
	{
		const string gameDir = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData";

		[TestMethod]
		public void ReadRhsFiles()
		{
			string[] files = Directory.GetFiles(gameDir, "*.rhs");
			List<RhsFile> rhsFiles = new List<RhsFile>();
			foreach (string file in files)
				rhsFiles.Add(ReadRhsFile(file));
		}

		[TestMethod]
		public void WriteRhsFileBytesEqualsOriginalRhsFileBytes()
		{
			string[] files = Directory.GetFiles(gameDir, "*.rhs");
			foreach (string file in files)
			{
				RhsFile rhsFile = ReadRhsFile(file);

				byte[] writeBytes = rhsFile.SerializeToBytes();
				byte[] originalBytes = File.ReadAllBytes(file);

				Assert.IsTrue(Enumerable.SequenceEqual(originalBytes, writeBytes));
			}
		}

		private RhsFile ReadRhsFile(string path)
		{
			return IBinarySerializableExtensions.Deserialize(path, RhsFile.Deserialize);
		}
	}
}
