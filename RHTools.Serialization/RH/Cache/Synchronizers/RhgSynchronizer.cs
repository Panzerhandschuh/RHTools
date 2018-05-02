using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class RhgSynchronizer : CacheSynchronizer<RhgCacheEntry>
	{
		private RhgFile rhgFile;

		public RhgSynchronizer(CacheFile cacheFile, RhgFile rhgFile) : base(cacheFile)
		{
			this.rhgFile = rhgFile;
		}

		protected override void AddEntry(RhgCacheEntry entry)
		{
			cacheFile.rhgEntries.Add(entry);
		}

		protected override RhgCacheEntry FindEntry()
		{
			return cacheFile.rhgEntries.SingleOrDefault(x => x.rhgGuid == rhgFile.rhgGuid);
		}

		protected override void Update(RhgCacheEntry entry)
		{
			entry.rhgGuid = rhgFile.rhgGuid;
			entry.internalGuid = rhgFile.internalGuid;
			entry.pngGuid = rhgFile.pngGuid;
			entry.packName = rhgFile.packName;
			entry.rhcGuids = rhgFile.rhcGuids;
		}
	}
}
