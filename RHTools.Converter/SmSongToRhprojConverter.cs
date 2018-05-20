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
			RhSongAssets assets = converter.Convert();
			SyncSongAssetsToCache(rhDir, assets);

			RhprojConverter rhprojConverter = new RhprojConverter();
			List<RhprojFile> rhprojFiles = rhprojConverter.Convert(assets.rhsFile, assets.rhcFiles);

			string tabsPath = Path.Combine(rhDir, "Backup", "tabs");
			TabsFile tabsFile = IBinarySerializableExtensions.Deserialize(tabsPath, TabsFile.Deserialize);

			foreach (RhprojFile rhprojFile in rhprojFiles)
			{
				RhGuid rhcGuid = rhprojFile.rhcFile.rhcGuid;
				string filename = rhcGuid.ToString() + ".rhproj";
				string destPath = Path.Combine(rhDir, "Backup", filename);
				rhprojFile.SerializeToFile(destPath);

				tabsFile.rhprojFileGuids.Add(rhcGuid);
			}

			tabsFile.SerializeToFile(tabsPath);
		}

		private void SyncSongAssetsToCache(string rhDir, RhSongAssets assets)
		{
			string cachePath = Path.Combine(rhDir, "cache");
			CacheFile cacheFile = IBinarySerializableExtensions.Deserialize(cachePath, CacheFile.Deserialize);

			RhSongAssetsSynchronizer songSynchronizer = new RhSongAssetsSynchronizer(cacheFile, assets);
			songSynchronizer.Sync();

			cacheFile.SerializeToFile(cachePath);
		}
	}
}
