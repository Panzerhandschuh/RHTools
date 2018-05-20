using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class RhcSynchronizer : CacheSynchronizer<RhcCacheEntry>
	{
		private RhcFile rhcFile;

		public RhcSynchronizer(CacheFile cacheFile, RhcFile rhcFile) : base(cacheFile)
		{
			this.rhcFile = rhcFile;
		}

		protected override void AddEntry(RhcCacheEntry entry)
		{
			cacheFile.rhcEntries.Add(entry);
		}

		protected override RhcCacheEntry FindEntry()
		{
			return cacheFile.rhcEntries.SingleOrDefault(x => x.rhcGuid == rhcFile.rhcGuid);
		}

		protected override void Update(RhcCacheEntry entry)
		{
			entry.rhcGuid = rhcFile.rhcGuid;
			entry.rhsGuid = rhcFile.rhsGuid;
			entry.internalGuid = rhcFile.internalGuid;
			entry.chartName = rhcFile.chartName;
			entry.unknown1 = new byte[] { 0, 0, 10, 254, 3, 0, 0 }; // Fake data
			entry.artists = rhcFile.artists;
			entry.unknown2 = new byte[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 8, 0, 0, 0, 0, 9, 0, 0, 0, 0, 10, 0, 0, 0, 0, 32, 0, 0, 0, 0, 33, 0, 0, 0, 0, 255 }; // Fake data
			entry.displayAuthor = rhcFile.artists.FirstOrDefault()?.artist ?? "";
		}
	}
}
