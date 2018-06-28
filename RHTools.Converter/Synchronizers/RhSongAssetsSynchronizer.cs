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
			var oggSynchronizer = new OggSynchronizer(cacheFile, assets.oggGuid, 0f);
			oggSynchronizer.Sync();

			var pngSynchronizer = new PngSynchronizer(cacheFile, assets.pngGuid);
			pngSynchronizer.Sync();

			var rhsSynchronizer = new RhsSynchronizer(cacheFile, assets.rhsFile);
			rhsSynchronizer.Sync();

			foreach (var rhcFile in assets.rhcFiles)
			{
				var rhcSynchronizer = new RhcSynchronizer(cacheFile, rhcFile);
				rhcSynchronizer.Sync();
			}
		}
	}
}
