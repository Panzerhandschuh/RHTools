using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.RHLib.RH;
using RHTools.RHLib.Serialization;

namespace RHTools.RHLib.Test
{
	[TestClass]
	public class CacheFileTest
	{
		const string cacheFilePath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\cache";

		[TestMethod]
		public void ReadCacheFile()
		{
			CacheFile file = ReadCacheFile(cacheFilePath);
		}

		[TestMethod]
		public void WriteCacheFileBytesEqualsOriginalCacheFileBytes()
		{
			CacheFile file = ReadCacheFile(cacheFilePath);

			byte[] writeBytes = file.SerializeToBytes();
			byte[] originalBytes = File.ReadAllBytes(cacheFilePath);

			Assert.IsTrue(Enumerable.SequenceEqual(originalBytes, writeBytes));
		}

		private static CacheFile ReadCacheFile(string path)
		{
			using (Stream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (BinaryReader reader = new BinaryReader(stream))
			{
				return CacheFile.Deserialize(reader);
			}
		}
	}
}
