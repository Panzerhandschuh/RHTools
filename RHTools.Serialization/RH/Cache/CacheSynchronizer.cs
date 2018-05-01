using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	// TODO: Refactor this file so that each cache entry type has a different synchronizer (single responsibility)
	// TODO: Might also be able to refactor Update/Create pattern into a shared base class
	public class CacheSynchronizer
	{
		private CacheFile cacheFile;

		public CacheSynchronizer(CacheFile cacheFile)
		{
			this.cacheFile = cacheFile;
		}

		public void SyncOggFile(RhGuid oggGuid)
		{
			OggCacheEntry existingEntry = cacheFile.oggEntries.FirstOrDefault(x => x.guid == oggGuid);
			if (existingEntry != null)
				UpdateOggCacheEntry(existingEntry, oggGuid);
			else
			{
				OggCacheEntry newEntry = CreateOggCacheEntry(oggGuid);
				cacheFile.oggEntries.Add(newEntry);
			}
		}

		private OggCacheEntry CreateOggCacheEntry(RhGuid oggGuid)
		{
			OggCacheEntry cacheEntry = new OggCacheEntry();

			UpdateOggCacheEntry(cacheEntry, oggGuid);

			return cacheEntry;
		}

		private void UpdateOggCacheEntry(OggCacheEntry cacheEntry, RhGuid oggGuid)
		{
			cacheEntry.guid = oggGuid;
			//cacheEntry.length = ;
		}

		public void SyncPngFile(RhGuid pngGuid)
		{
			PngCacheEntry existingEntry = cacheFile.pngEntries.FirstOrDefault(x => x.guid == pngGuid);
			if (existingEntry != null)
				UpdatePngCacheEntry(existingEntry, pngGuid);
			else
			{
				PngCacheEntry newEntry = CreatePngCacheEntry(pngGuid);
				cacheFile.pngEntries.Add(newEntry);
			}
		}

		private PngCacheEntry CreatePngCacheEntry(RhGuid pngGuid)
		{
			PngCacheEntry cacheEntry = new PngCacheEntry();

			UpdatePngCacheEntry(cacheEntry, pngGuid);

			return cacheEntry;
		}

		private void UpdatePngCacheEntry(PngCacheEntry cacheEntry, RhGuid pngGuid)
		{
			cacheEntry.guid = pngGuid;
		}

		public void SyncRhcFile(RhcFile rhcFile)
		{
			RhcCacheEntry existingEntry = cacheFile.rhcEntries.FirstOrDefault(x => x.rhcGuid == rhcFile.rhcGuid);
			if (existingEntry != null)
				UpdateRhcCacheEntry(existingEntry, rhcFile);
			else
			{
				RhcCacheEntry newEntry = CreateRhcCacheEntry(rhcFile);
				cacheFile.rhcEntries.Add(newEntry);
			}
		}

		private RhcCacheEntry CreateRhcCacheEntry(RhcFile rhcFile)
		{
			RhcCacheEntry cacheEntry = new RhcCacheEntry();

			UpdateRhcCacheEntry(cacheEntry, rhcFile);

			return cacheEntry;
		}

		private static void UpdateRhcCacheEntry(RhcCacheEntry cacheEntry, RhcFile rhcFile)
		{
			cacheEntry.rhcGuid = rhcFile.rhcGuid;
			cacheEntry.rhsGuid = rhcFile.rhsGuid;
			cacheEntry.internalGuid = rhcFile.internalGuid;
			cacheEntry.chartName = rhcFile.chartName;
			//cacheEntry.unknown1 = rhcFile.unknown1;
			cacheEntry.artists = rhcFile.artists;
			//cacheEntry.unknown2 = rhcFile.unknown1;
			//cacheEntry.displayAuthor = ; // Missing
		}

		public void SyncRhgFile(RhgFile rhgFile)
		{
			RhgCacheEntry existingEntry = cacheFile.rhgEntries.FirstOrDefault(x => x.rhgGuid == rhgFile.rhgGuid);
			if (existingEntry != null)
				UpdateRhgCacheEntry(existingEntry, rhgFile);
			else
			{
				RhgCacheEntry newEntry = CreateRhgCacheEntry(rhgFile);
				cacheFile.rhgEntries.Add(newEntry);
			}
		}

		private RhgCacheEntry CreateRhgCacheEntry(RhgFile rhgFile)
		{
			RhgCacheEntry cacheEntry = new RhgCacheEntry();

			UpdateRhgCacheEntry(cacheEntry, rhgFile);

			return cacheEntry;
		}

		private void UpdateRhgCacheEntry(RhgCacheEntry cacheEntry, RhgFile rhgFile)
		{
			cacheEntry.rhgGuid = rhgFile.rhgGuid;
			cacheEntry.internalGuid = rhgFile.internalGuid;
			cacheEntry.pngGuid = rhgFile.pngGuid;
			cacheEntry.packName = rhgFile.packName;
			cacheEntry.rhcGuids = rhgFile.rhcGuids;
		}

		public void SyncRhsFile(RhsFile rhsFile)
		{
			RhsCacheEntry existingEntry = cacheFile.rhsEntries.FirstOrDefault(x => x.rhsGuid == rhsFile.rhsGuid);
			if (existingEntry != null)
				UpdateRhsCacheEntry(existingEntry, rhsFile);
			else
			{
				RhsCacheEntry newEntry = CreateRhsCacheEntry(rhsFile);
				cacheFile.rhsEntries.Add(newEntry);
			}
		}

		private RhsCacheEntry CreateRhsCacheEntry(RhsFile rhsFile)
		{
			RhsCacheEntry cacheEntry = new RhsCacheEntry();

			UpdateRhsCacheEntry(cacheEntry, rhsFile);

			return cacheEntry;
		}

		private void UpdateRhsCacheEntry(RhsCacheEntry cacheEntry, RhsFile rhsFile)
		{
			cacheEntry.rhsGuid = rhsFile.rhsGuid;
			cacheEntry.internalGuid = rhsFile.internalGuid;
			cacheEntry.oggGuid = rhsFile.oggGuid;
			cacheEntry.pngGuid = rhsFile.pngGuid;
			cacheEntry.songTitle = rhsFile.songTitle;
			cacheEntry.timingData = rhsFile.timingData;
			cacheEntry.previewStart = rhsFile.previewStart;
			cacheEntry.previewLength = rhsFile.previewLength;
			cacheEntry.displayBpm = rhsFile.displayBpm;
			cacheEntry.artists = rhsFile.artists;
			//cacheEntry.displayArtist = ; // Missing
		}
	}
}
