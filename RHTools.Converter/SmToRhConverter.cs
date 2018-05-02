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

			RhsConverter rhsConverter = new RhsConverter();
			RhsFile rhsFile = rhsConverter.Convert(smFile, oggGuid, pngGuid);
			string rhsPath = Path.Combine(rhDir, rhsFile.rhsGuid.ToString()) + ".rhs";
			rhsFile.SerializeToFile(rhsPath);

			RhsSynchronizer rhsSynchronizer = new RhsSynchronizer(cacheFile, rhsFile, smFile.artist);
			rhsSynchronizer.Sync();

			RhcConverter rhcConverter = new RhcConverter();
			List<RhcFile> rhcFiles = rhcConverter.Convert(smFile, rhsFile.rhsGuid, smFile.credit);
			foreach (RhcFile rhcFile in rhcFiles)
			{
				string rhcPath = Path.Combine(rhDir, rhcFile.rhcGuid.ToString()) + ".rhc";
				rhcFile.SerializeToFile(rhcPath);

				RhcSynchronizer rhcSynchronizer = new RhcSynchronizer(cacheFile, rhcFile, smFile.credit);
				rhcSynchronizer.Sync();
			}

			RhgConverter rhgConverter = new RhgConverter();
			RhgFile rhgFile = rhgConverter.Convert(pngGuid, packName, rhcFiles);
			string rhgPath = Path.Combine(rhDir, rhgFile.rhgGuid.ToString()) + ".rhg";
			rhgFile.SerializeToFile(rhgPath);

			RhgSynchronizer rhgSynchronizer = new RhgSynchronizer(cacheFile, rhgFile);
			rhgSynchronizer.Sync();

			cacheFile.SerializeToFile(cachePath);

			//string onlinePath = Path.Combine(rhDir, "online");
			//OnlineFile onlineFile = IBinarySerializableExtensions.Deserialize(onlinePath, OnlineFile.Deserialize);
			//onlineFile.fileGuids.Add(rhsFile.rhsGuid);
			//onlineFile.fileGuids.AddRange(rhcFiles.Select(x => x.rhcGuid));
			//onlineFile.fileGuids.Add(rhgFile.rhgGuid);
			//onlineFile.SerializeToFile(onlinePath);
		}
	}
}
