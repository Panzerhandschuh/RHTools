using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.RHLib.RH;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.Test.RH
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
