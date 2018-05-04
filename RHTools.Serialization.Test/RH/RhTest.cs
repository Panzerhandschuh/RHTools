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
		const string rhPath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData";

		[TestMethod]
		public void EditTimingData()
		{
			string cachePath = Path.Combine(rhPath, "cache");
			CacheFile cacheFile = IBinarySerializableExtensions.Deserialize(cachePath, CacheFile.Deserialize);

			string rhsPath = Path.Combine(rhPath, "24a33e2b-cc61-4df1-8f7c-d7fc844450b2.rhs");
			RhsFile rhsFile = IBinarySerializableExtensions.Deserialize(rhsPath, RhsFile.Deserialize);

			//rhsFile.internalGuid = new RhGuid();
			//rhsFile.timingData.entries = new List<TimingDataEntry>();
			//TimingData rhsTimingData = rhsFile.timingData;
			//rhsTimingData.unknown1 = 5;
			//rhsTimingData.unknown2 = 0;
			//TimingDataEntry rhsTimingDataEntry = rhsFile.timingData.entries.Last();
			//rhsTimingDataEntry.startBpm = -1;
			//rhsTimingDataEntry.endBpm = 1f;
			rhsFile.songLengthOverride = -1;
			rhsFile.pngGuid = new RhGuid(new byte[] { 38, 202, 1, 86, 253, 228, 71, 86, 152, 232, 0, 213, 9, 179, 44, 201 });
			rhsFile.SerializeToFile(rhsPath);

			RhsCacheEntry cacheEntry = cacheFile.rhsEntries.Single(x => x.rhsGuid == rhsFile.rhsGuid);
			//cacheEntry.internalGuid = new RhGuid();
			//cacheEntry.timingData.entries = new List<TimingDataEntry>();
			//TimingData cacheTimingData = cacheEntry.timingData;
			//cacheTimingData.unknown1 = 5;
			//cacheTimingData.unknown2 = 0;
			//TimingDataEntry cacheTimingDataEntry = cacheEntry.timingData.entries.Last();
			//cacheTimingDataEntry.startBpm = -1f;
			//cacheTimingDataEntry.endBpm = 1f;
			cacheEntry.pngGuid = new RhGuid(new byte[] { 38, 202, 1, 86, 253, 228, 71, 86, 152, 232, 0, 213, 9, 179, 44, 201 });
			cacheFile.SerializeToFile(cachePath);
		}
	}
}
