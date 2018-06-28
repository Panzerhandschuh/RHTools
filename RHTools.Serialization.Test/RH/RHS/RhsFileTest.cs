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
		private const string gameDir = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData";

		[TestMethod]
		public void ReadRhsFiles()
		{
			var files = Directory.GetFiles(gameDir, "*.rhs");
			var rhsFiles = new List<RhsFile>();
			foreach (var file in files)
				rhsFiles.Add(ReadRhsFile(file));
		}

		[TestMethod]
		public void WriteRhsFileBytesEqualsOriginalRhsFileBytes()
		{
			var files = Directory.GetFiles(gameDir, "*.rhs");
			foreach (var file in files)
			{
				var rhsFile = ReadRhsFile(file);

				var writeBytes = rhsFile.SerializeToBytes();
				var originalBytes = File.ReadAllBytes(file);

				Assert.IsTrue(Enumerable.SequenceEqual(originalBytes, writeBytes));
			}
		}

		private RhsFile ReadRhsFile(string path)
		{
			return IBinarySerializableExtensions.Deserialize(path, RhsFile.Deserialize);
		}
	}
}
