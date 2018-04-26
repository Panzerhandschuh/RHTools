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
			CacheFile file = DeserializeCacheFile();
		}

		[TestMethod]
		public void WriteCacheFileBytesEqualsOriginalCacheFileBytes()
		{
			CacheFile file = DeserializeCacheFile();

			byte[] writeBytes;
			using (MemoryStream stream = new MemoryStream())
			using (BinaryWriter writer = new BinaryWriter(stream))
			{
				file.Serialize(writer);
				writeBytes = stream.ToArray();
			}

			byte[] originalBytes = File.ReadAllBytes(cacheFilePath);
			Assert.IsTrue(Enumerable.SequenceEqual(originalBytes, writeBytes));
		}

		private static CacheFile DeserializeCacheFile()
		{
			using (Stream stream = File.Open(cacheFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (BinaryReader reader = new BinaryReader(stream))
			{
				return CacheFile.Deserialize(reader);
			}
		}
	}
}
