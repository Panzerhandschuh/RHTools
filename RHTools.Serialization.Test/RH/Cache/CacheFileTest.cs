using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.Serialization.RH;

namespace RHTools.Serialization.Test.RH
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
			return IBinarySerializableExtensions.Deserialize(path, CacheFile.Deserialize);
		}
	}
}
