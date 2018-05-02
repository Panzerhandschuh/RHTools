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
		private string displayArtist;

		public RhsSynchronizer(CacheFile cacheFile, RhsFile rhsFile, string displayArtist) : base(cacheFile)
		{
			this.rhsFile = rhsFile;
			this.displayArtist = displayArtist;
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
			entry.displayBpm = rhsFile.displayBpm;
			entry.artists = rhsFile.artists;
			entry.displayArtist = displayArtist;
		}
	}
}
