using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.RHLib.RH;

namespace RHTools.RHLib.Test
{
	[TestClass]
	public class CacheFileTest
	{
		const string cacheFilePath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\cache";

		[TestMethod]
		public void ReadCacheFile()
		{
			using (Stream stream = File.Open(cacheFilePath, FileMode.Open))
			using (BinaryReader reader = new BinaryReader(stream))
			{
				CacheFile file = CacheFile.Deserialize(reader);
			}
		}
	}
}
