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
		private string displayAuthor;

		public RhcSynchronizer(CacheFile cacheFile, RhcFile rhcFile, string displayAuthor) : base(cacheFile)
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
			//entry.unknown1 = rhcFile.unknown1;
			entry.artists = rhcFile.artists;
			//entry.unknown2 = rhcFile.unknown1;
			entry.displayAuthor = displayAuthor;
		}
	}
}
