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
	public class RhprojFileTest
	{
		const string gameDir = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData";
		const string backupDir = gameDir + @"\Backup";

		[TestMethod]
		public void ReadRhprojFile()
		{
			RhprojFile file = ReadRhprojFile(@"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\Backup\6939cbd5-4e31-4ea6-809a-402af8f2ca8a.rhproj");
		}

		[TestMethod]
		public void ReadRhprojFiles()
		{
			string[] files = Directory.GetFiles(backupDir, "*.rhproj");
			List<RhprojFile> rhprojFiles = new List<RhprojFile>();
			foreach (string file in files)
				rhprojFiles.Add(ReadRhprojFile(file));
		}

		[TestMethod]
		public void WriteRhprojFileBytesEqualsOriginalRhprojFileBytes()
		{
			string[] files = Directory.GetFiles(backupDir, "*.rhproj");
			foreach (string file in files)
			{
				RhprojFile rhprojFile = ReadRhprojFile(file);

				byte[] writeBytes = rhprojFile.SerializeToBytes();
				byte[] originalBytes = File.ReadAllBytes(file);

				Assert.IsTrue(Enumerable.SequenceEqual(originalBytes, writeBytes));
			}
		}

		[TestMethod]
		public void EditRhprojData()
		{
			string testFile = Path.Combine(backupDir, "f6e66110-bac2-4bf8-ae13-65b40ea2bb3a.rhproj");
			RhprojFile file = ReadRhprojFile(testFile);

			string rhcPath = Path.Combine(gameDir, "df76ece8-b8f9-1a41-9651-01bbcb3308f0.rhc");
			RhcFile rhcFile = IBinarySerializableExtensions.Deserialize(rhcPath, RhcFile.Deserialize);

			file.rhcFile = rhcFile;

			string rhsPath = Path.Combine(gameDir, "e10133c1-a7eb-f749-9c03-95591ad0e5ae.rhs");
			RhsFile rhsFile = IBinarySerializableExtensions.Deserialize(rhsPath, RhsFile.Deserialize);

			file.rhsFile = rhsFile;

			file.SerializeToFile(testFile);
		}

		private RhprojFile ReadRhprojFile(string path)
		{
			return IBinarySerializableExtensions.Deserialize(path, RhprojFile.Deserialize);
		}
	}
}
