using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.Serialization.Extensions;
using RHTools.Serialization.SM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.Test.SM
{
	[TestClass]
	public class SmFileTest
	{
		const string smFilePath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\Butterfly.sm";
		const string smPath = @"D:\Program Files (x86)\Stepmania 3.95\Songs";

		[TestMethod]
		public void ReadSmFiles()
		{
			string[] files = Directory.GetFiles(smPath, "*.sm", SearchOption.AllDirectories);
			List<SmFile> smFiles = new List<SmFile>();
			foreach (string file in files)
				smFiles.Add(ReadSmFile(file));
		}

		[TestMethod]
		public void ReadSmFile()
		{
			SmFile file = ReadSmFile(smFilePath);
		}

		private static SmFile ReadSmFile(string path)
		{
			return ITextSerializableExtensions.Deserialize(path, SmFile.Deserialize);
		}
	}
}
