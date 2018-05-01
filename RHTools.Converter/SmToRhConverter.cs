using RHTools.Serialization;
using RHTools.Serialization.Extensions;
using RHTools.Serialization.RH;
using RHTools.Serialization.SM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public class SmToRhConverter
	{
		public void ConvertSmPack(string smPackDir, string rhDir)
		{
			throw new NotImplementedException();
		}

		// TODO: This method is too big
		public void ConvertSmFile(string smFileDir, string rhDir, string packName = "Converted")
		{
			string smFilePath = Directory.GetFiles(smFileDir, "*.sm").Single();
			SmFile smFile = ITextSerializableExtensions.Deserialize(smFilePath, SmFile.Deserialize);

			RhGuid musicGuid = RhGuid.NewGuid();
			string sourceMusicPath = Path.Combine(smFileDir, smFile.music);
			string destMusicPath = Path.Combine(rhDir, musicGuid.ToString()) + ".ogg";
			File.Copy(sourceMusicPath, destMusicPath);

			RhGuid backgroundGuid = RhGuid.NewGuid();
			string sourceBackgroundPath = Path.Combine(smFileDir, smFile.background);
			string destBackgroundPath = Path.Combine(rhDir, backgroundGuid.ToString()) + ".png";
			File.Copy(sourceBackgroundPath, destBackgroundPath);

			RhsConverter rhsConverter = new RhsConverter();
			RhsFile rhsFile = rhsConverter.Convert(smFile, musicGuid, backgroundGuid);
			string rhsPath = Path.Combine(rhDir, rhsFile.rhsGuid.ToString()) + ".rhs";
			rhsFile.SerializeToFile(rhsPath);

			string cachePath = Path.Combine(rhDir, "cache");
			CacheFile cacheFile = IBinarySerializableExtensions.Deserialize(cachePath, CacheFile.Deserialize);

			CacheSynchronizer synchronizer = new CacheSynchronizer(cacheFile);
			synchronizer.SyncOggFile(musicGuid);
			synchronizer.SyncPngFile(backgroundGuid);
			synchronizer.SyncRhsFile(rhsFile);

			cacheFile.SerializeToFile(cachePath);
		}
	}
}
