﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
	public class RhcFileTest
	{
		private const string gameDir = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData";

		[TestMethod]
		public void ReadRhcFile()
		{
			var file = ReadRhcFile(@"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\00f1652d-a278-4f61-a63b-7a58cd8c66c0.rhc");
		}

		[TestMethod]
		public void ReadRhcFiles()
		{
			var files = Directory.GetFiles(gameDir, "*.rhc");
			var rhcFiles = new List<RhcFile>();
			foreach (var file in files)
				rhcFiles.Add(ReadRhcFile(file));
		}

		[TestMethod]
		public void WriteRhcFileBytesEqualsOriginalRhcFileBytes()
		{
			var files = Directory.GetFiles(gameDir, "*.rhc");
			foreach (var file in files)
			{
				var rhcFile = ReadRhcFile(file);

				var writeBytes = rhcFile.SerializeToBytes();
				var originalBytes = File.ReadAllBytes(file);

				Assert.IsTrue(Enumerable.SequenceEqual(originalBytes, writeBytes));
			}
		}

		private RhcFile ReadRhcFile(string path)
		{
			return IBinarySerializableExtensions.Deserialize(path, RhcFile.Deserialize);
		}
	}
}
