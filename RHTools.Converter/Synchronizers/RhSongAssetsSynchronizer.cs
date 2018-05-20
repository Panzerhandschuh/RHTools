using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter.Synchronizers
{
	public class RhSongAssetsSynchronizer
	{
		private CacheFile cacheFile;
		private RhSongAssets assets;

		public RhSongAssetsSynchronizer(CacheFile cacheFile, RhSongAssets assets)
		{
			this.cacheFile = cacheFile;
			this.assets = assets;
		}

		public void Sync()
		{
			OggSynchronizer oggSynchronizer = new OggSynchronizer(cacheFile, assets.oggGuid, 0f);
			oggSynchronizer.Sync();

			PngSynchronizer pngSynchronizer = new PngSynchronizer(cacheFile, assets.pngGuid);
			pngSynchronizer.Sync();

			RhsSynchronizer rhsSynchronizer = new RhsSynchronizer(cacheFile, assets.rhsFile);
			rhsSynchronizer.Sync();

			foreach (RhcFile rhcFile in assets.rhcFiles)
			{
				RhcSynchronizer rhcSynchronizer = new RhcSynchronizer(cacheFile, rhcFile);
				rhcSynchronizer.Sync();
			}
		}
	}
}
