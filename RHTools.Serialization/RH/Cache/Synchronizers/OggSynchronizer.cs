using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class OggSynchronizer : CacheSynchronizer<OggCacheEntry>
	{
		private RhGuid oggGuid;
		private float length;

		public OggSynchronizer(CacheFile cacheFile, RhGuid oggGuid, float length) : base(cacheFile)
		{
			this.oggGuid = oggGuid;
			this.length = length;
		}

		protected override void AddEntry(OggCacheEntry entry)
		{
			cacheFile.oggEntries.Add(entry);
		}

		protected override OggCacheEntry FindEntry()
		{
			return cacheFile.oggEntries.SingleOrDefault(x => x.oggGuid == oggGuid);
		}

		protected override void Update(OggCacheEntry entry)
		{
			entry.oggGuid = oggGuid;
			entry.length = length;
		}
	}
}
