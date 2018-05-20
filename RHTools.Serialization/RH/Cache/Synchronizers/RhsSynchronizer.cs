using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class RhsSynchronizer : CacheSynchronizer<RhsCacheEntry>
	{
		private RhsFile rhsFile;

		public RhsSynchronizer(CacheFile cacheFile, RhsFile rhsFile) : base(cacheFile)
		{
			this.rhsFile = rhsFile;
		}

		protected override void AddEntry(RhsCacheEntry entry)
		{
			cacheFile.rhsEntries.Add(entry);
		}

		protected override RhsCacheEntry FindEntry()
		{
			return cacheFile.rhsEntries.SingleOrDefault(x => x.rhsGuid == rhsFile.rhsGuid);
		}

		protected override void Update(RhsCacheEntry entry)
		{
			entry.rhsGuid = rhsFile.rhsGuid;
			entry.internalGuid = rhsFile.internalGuid;
			entry.oggGuid = rhsFile.oggGuid;
			entry.pngGuid = rhsFile.pngGuid;
			entry.songTitle = rhsFile.songTitle;
			entry.timingData = rhsFile.timingData;
			entry.previewStart = rhsFile.previewStart;
			entry.previewLength = rhsFile.previewLength;
			entry.songLengthOverride = rhsFile.songLengthOverride;
			entry.artists = rhsFile.artists;
			entry.displayArtist = rhsFile.artists.FirstOrDefault()?.artist ?? "";
		}
	}
}
