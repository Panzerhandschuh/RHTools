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
		const string backupDir = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\Backup";

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
			const string testFile = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\Backup\01c19d82-d894-453c-9cbf-e65b70540926.rhproj";

			RhprojFile file = ReadRhprojFile(testFile);

			RhsFile rhsFile = file.rhsFile;
			rhsFile.songTitle = "[Tech::Lo] Test";
			rhsFile.songLengthOverride = 25f;

			TimingData timingData = rhsFile.timingData;
			timingData.offsetMultiplier = 0;

			List<TimingDataEntry> entries = timingData.entries;
			entries[0].startBpm *= 2;

			file.SerializeToFile(testFile);
		}

		private RhprojFile ReadRhprojFile(string path)
		{
			return IBinarySerializableExtensions.Deserialize(path, RhprojFile.Deserialize);
		}
	}
}
