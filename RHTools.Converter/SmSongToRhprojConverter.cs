using RHTools.Converter.Synchronizers;
using RHTools.Serialization;
using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public class SmSongToRhprojConverter
	{
		private string rhDir;
		private SmSongToRhConverter converter;

		public SmSongToRhprojConverter(string smSongDir, string rhDir, float songOffset)
		{
			this.rhDir = rhDir;

			converter = new SmSongToRhConverter(smSongDir, rhDir, songOffset);
		}

		public void Convert()
		{
			var assets = converter.Convert();
			SyncSongAssetsToCache(rhDir, assets);

			var rhprojConverter = new RhprojConverter();
			var rhprojFiles = rhprojConverter.Convert(assets.rhsFile, assets.rhcFiles);

			var tabsPath = Path.Combine(rhDir, "Backup", "tabs");
			var tabsFile = IBinarySerializableExtensions.Deserialize(tabsPath, TabsFile.Deserialize);

			foreach (var rhprojFile in rhprojFiles)
			{
				var rhcGuid = rhprojFile.rhcFile.rhcGuid;
				var filename = rhcGuid.ToString() + ".rhproj";
				var destPath = Path.Combine(rhDir, "Backup", filename);
				rhprojFile.SerializeToFile(destPath);

				tabsFile.rhprojFileGuids.Insert(0, rhcGuid);
			}

			tabsFile.SerializeToFile(tabsPath);
		}

		private void SyncSongAssetsToCache(string rhDir, RhSongAssets assets)
		{
			var cachePath = Path.Combine(rhDir, "cache");
			var cacheFile = IBinarySerializableExtensions.Deserialize(cachePath, CacheFile.Deserialize);

			var songSynchronizer = new RhSongAssetsSynchronizer(cacheFile, assets);
			songSynchronizer.Sync();

			cacheFile.SerializeToFile(cachePath);
		}
	}
}
