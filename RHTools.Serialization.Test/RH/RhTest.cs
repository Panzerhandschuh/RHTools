using Microsoft.VisualStudio.TestTools.UnitTesting;
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
	public class RhTest
	{
		private const string rhPath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData";

		[TestMethod]
		public void EditTimingData()
		{
			var rhsPath = Path.Combine(rhPath, "0174eed2-8658-4b48-87e4-7e5f0753b79d.rhs");
			var rhsFile = IBinarySerializableExtensions.Deserialize(rhsPath, RhsFile.Deserialize);

			rhsFile.internalGuid = new RhGuid();
			rhsFile.timingData.offsetMultiplier = 0;
			//rhsFile.timingData.entries[0].startBpm /= 2;
			//rhsFile.pngGuid = new RhGuid(new byte[] { 38, 202, 1, 86, 253, 228, 71, 86, 152, 232, 0, 213, 9, 179, 44, 201 });
			rhsFile.SerializeToFile(rhsPath);

			var cachePath = Path.Combine(rhPath, "cache");
			var rhsGuid = new RhGuid(new byte[] { 0x01, 0x74, 0xee, 0xd2, 0x86, 0x58, 0x4b, 0x48, 0x87, 0xe4, 0x7e, 0x5f, 0x07, 0x53, 0xb7, 0x9d });
			var cacheFile = IBinarySerializableExtensions.Deserialize(cachePath, CacheFile.Deserialize);

			var rhsSynchronizer = new RhsSynchronizer(cacheFile, rhsFile);
			rhsSynchronizer.Sync();
			
			cacheFile.SerializeToFile(cachePath);
		}
	}
}
