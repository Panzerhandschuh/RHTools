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
