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
		private const string cacheFilePath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\cache";

		[TestMethod]
		public void ReadCacheFile()
		{
			var file = ReadCacheFile(cacheFilePath);
		}

		[TestMethod]
		public void WriteCacheFileBytesEqualsOriginalCacheFileBytes()
		{
			var file = ReadCacheFile(cacheFilePath);

			var writeBytes = file.SerializeToBytes();
			var originalBytes = File.ReadAllBytes(cacheFilePath);

			Assert.IsTrue(Enumerable.SequenceEqual(originalBytes, writeBytes));
		}

		private static CacheFile ReadCacheFile(string path)
		{
			return IBinarySerializableExtensions.Deserialize(path, CacheFile.Deserialize);
		}
	}
}
