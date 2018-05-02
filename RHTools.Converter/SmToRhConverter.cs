﻿using RHTools.Serialization;
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
			string cachePath = Path.Combine(rhDir, "cache");
			CacheFile cacheFile = IBinarySerializableExtensions.Deserialize(cachePath, CacheFile.Deserialize);

			string smFilePath = Directory.GetFiles(smFileDir, "*.sm").Single();
			SmFile smFile = ITextSerializableExtensions.Deserialize(smFilePath, SmFile.Deserialize);

			RhGuid oggGuid = RhGuid.NewGuid();
			string sourceOggPath = Path.Combine(smFileDir, smFile.music);
			string destOggPath = Path.Combine(rhDir, oggGuid.ToString()) + ".ogg";
			File.Copy(sourceOggPath, destOggPath);

			OggSynchronizer oggSynchronizer = new OggSynchronizer(cacheFile, oggGuid, 0f);
			oggSynchronizer.Sync();

			RhGuid pngGuid = RhGuid.NewGuid();
			string sourcePngPath = Path.Combine(smFileDir, smFile.background);
			string destPngPath = Path.Combine(rhDir, pngGuid.ToString()) + ".png";
			File.Copy(sourcePngPath, destPngPath);

			PngSynchronizer pngSynchronizer = new PngSynchronizer(cacheFile, pngGuid);
			pngSynchronizer.Sync();

			//RhsConverter rhsConverter = new RhsConverter();
			//RhsFile rhsFile = rhsConverter.Convert(smFile, musicGuid, backgroundGuid);
			//string rhsPath = Path.Combine(rhDir, rhsFile.rhsGuid.ToString()) + ".rhs";
			//rhsFile.SerializeToFile(rhsPath);

			//RhsSynchronizer rhsSynchronizer = new RhsSynchronizer(cacheFile, rhsFile, smFile.artist);
			//rhsSynchronizer.Sync();

			RhgConverter rhgConverter = new RhgConverter();
			RhgFile rhgFile = rhgConverter.Convert(pngGuid, packName, new List<RhcFile>());
			string rhgPath = Path.Combine(rhDir, rhgFile.rhgGuid.ToString()) + ".rhg";
			rhgFile.SerializeToFile(rhgPath);

			RhgSynchronizer rhgSynchronizer = new RhgSynchronizer(cacheFile, rhgFile);
			rhgSynchronizer.Sync();

			//CacheSynchronizerOld synchronizer = new CacheSynchronizerOld(cacheFile);
			//synchronizer.SyncOggFile(oggGuid);
			//synchronizer.SyncPngFile(pngGuid);
			////synchronizer.SyncRhsFile(rhsFile);
			//synchronizer.SyncRhgFile(rhgFile);

			cacheFile.SerializeToFile(cachePath);
		}
	}
}