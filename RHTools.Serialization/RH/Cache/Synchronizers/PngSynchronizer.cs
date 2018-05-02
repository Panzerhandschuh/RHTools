using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class PngSynchronizer : CacheSynchronizer<PngCacheEntry>
	{
		private RhGuid pngGuid;

		public PngSynchronizer(CacheFile cacheFile, RhGuid pngGuid) : base(cacheFile)
		{
			this.pngGuid = pngGuid;
		}

		protected override void AddEntry(PngCacheEntry entry)
		{
			cacheFile.pngEntries.Add(entry);
		}

		protected override PngCacheEntry FindEntry()
		{
			return cacheFile.pngEntries.SingleOrDefault(x => x.pngGuid == pngGuid);
		}

		protected override void Update(PngCacheEntry entry)
		{
			entry.pngGuid = pngGuid;
		}
	}
}
