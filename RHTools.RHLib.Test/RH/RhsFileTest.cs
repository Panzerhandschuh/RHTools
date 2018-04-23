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
	public class RhsFileTest
	{
		const string rhsDir = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData"; // HeHeHe / CardboardBox

		[TestMethod]
		public void ReadRhsFiles()
		{
			string[] files = Directory.GetFiles(rhsDir, "*.rhs");
			List<RhsFile> rhsFiles = new List<RhsFile>();
			foreach (string file in files)
				rhsFiles.Add(ReadRhsFile(file));
		}

		private RhsFile ReadRhsFile(string path)
		{
			using (Stream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (BinaryReader reader = new BinaryReader(stream))
			{
				return RhsFile.Deserialize(reader);
			}
		}
	}
}
