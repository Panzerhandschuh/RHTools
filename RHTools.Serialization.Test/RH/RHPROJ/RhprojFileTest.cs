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
		const string testFile = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\Backup\6939cbd5-4e31-4ea6-809a-402af8f2ca8a.rhproj";

		[TestMethod]
		public void ReadRhprojFile()
		{
			RhprojFile file = ReadRhprojFile(testFile);
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
			RhprojFile file = ReadRhprojFile(testFile);

			RhsFile rhsFile = file.rhsFile;
			rhsFile.songTitle = "test";
			rhsFile.songLengthOverride = -1f;

			TimingData timingData = rhsFile.timingData;
			timingData.unknown1 = 0;
			timingData.offsetMultiplier = -256;

			List<TimingDataEntry> entries = timingData.entries;

			file.SerializeToFile(testFile);
		}

		private RhprojFile ReadRhprojFile(string path)
		{
			return IBinarySerializableExtensions.Deserialize(path, RhprojFile.Deserialize);
		}
	}
}
