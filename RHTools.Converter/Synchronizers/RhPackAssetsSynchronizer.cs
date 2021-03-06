﻿using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter.Synchronizers
{
	public class RhPackAssetsSynchronizer
	{
		private CacheFile cacheFile;
		private RhPackAssets assets;

		public RhPackAssetsSynchronizer(CacheFile cacheFile, RhPackAssets assets)
		{
			this.cacheFile = cacheFile;
			this.assets = assets;
		}

		public void Sync()
		{
			foreach (var songAssets in assets.songAssetList)
			{
				var songSynchronizer = new RhSongAssetsSynchronizer(cacheFile, songAssets);
				songSynchronizer.Sync();
			}

			var rhgSynchronizer = new RhgSynchronizer(cacheFile, assets.rhgFile);
			rhgSynchronizer.Sync();
		}
	}
}
