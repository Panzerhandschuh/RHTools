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
	public class SmSongToRhConverter
	{
		private string smSongDir;
		private string rhDir;
		private string packName;
		private SmFile smFile;
		private string cachePath;
		private CacheFile cacheFile;

		public SmSongToRhConverter(string smSongDir, string rhDir, string packName = "Converted")
		{
			this.smSongDir = smSongDir;
			this.rhDir = rhDir;
			this.packName = packName;

			string smFilePath = Directory.GetFiles(smSongDir, "*.sm").Single();
			smFile = ITextSerializableExtensions.Deserialize(smFilePath, SmFile.Deserialize);

			cachePath = Path.Combine(rhDir, "cache");
			cacheFile = IBinarySerializableExtensions.Deserialize(cachePath, CacheFile.Deserialize);
		}

		// TODO: Consider making all converters override a Convert() method
		public void Convert()
		{
			RhGuid oggGuid = ConvertOgg();
			RhGuid pngGuid = ConvertPng();
			RhsFile rhsFile = ConvertRhs(oggGuid, pngGuid);
			List<RhcFile> rhcFiles = ConvertRhc(rhsFile);
			ConvertRhg(pngGuid, rhcFiles);

			cacheFile.SerializeToFile(cachePath);
		}

		private RhGuid ConvertOgg()
		{
			RhGuid oggGuid = RhGuid.NewGuid();
			string sourceOggPath = Path.Combine(smSongDir, smFile.music);
			string destOggPath = Path.Combine(rhDir, oggGuid.ToString()) + ".ogg";
			File.Copy(sourceOggPath, destOggPath);

			OggSynchronizer oggSynchronizer = new OggSynchronizer(cacheFile, oggGuid, 0f);
			oggSynchronizer.Sync();
			return oggGuid;
		}

		private RhGuid ConvertPng()
		{
			RhGuid pngGuid = RhGuid.NewGuid();
			string sourcePngPath = Path.Combine(smSongDir, smFile.background);
			string destPngPath = Path.Combine(rhDir, pngGuid.ToString()) + ".png";
			File.Copy(sourcePngPath, destPngPath);

			PngSynchronizer pngSynchronizer = new PngSynchronizer(cacheFile, pngGuid);
			pngSynchronizer.Sync();
			return pngGuid;
		}

		private RhsFile ConvertRhs(RhGuid oggGuid, RhGuid pngGuid)
		{
			RhsConverter rhsConverter = new RhsConverter();
			RhsFile rhsFile = rhsConverter.Convert(smFile, oggGuid, pngGuid);
			string rhsPath = Path.Combine(rhDir, rhsFile.rhsGuid.ToString()) + ".rhs";
			rhsFile.SerializeToFile(rhsPath);

			RhsSynchronizer rhsSynchronizer = new RhsSynchronizer(cacheFile, rhsFile, smFile.artist);
			rhsSynchronizer.Sync();
			return rhsFile;
		}

		private List<RhcFile> ConvertRhc(RhsFile rhsFile)
		{
			RhcConverter rhcConverter = new RhcConverter();
			List<RhcFile> rhcFiles = rhcConverter.Convert(smFile, rhsFile.rhsGuid, smFile.credit);
			foreach (RhcFile rhcFile in rhcFiles)
			{
				string rhcPath = Path.Combine(rhDir, rhcFile.rhcGuid.ToString()) + ".rhc";
				rhcFile.SerializeToFile(rhcPath);

				RhcSynchronizer rhcSynchronizer = new RhcSynchronizer(cacheFile, rhcFile, smFile.credit);
				rhcSynchronizer.Sync();
			}

			return rhcFiles;
		}

		private void ConvertRhg(RhGuid pngGuid, List<RhcFile> rhcFiles)
		{
			RhgConverter rhgConverter = new RhgConverter();
			RhgFile rhgFile = rhgConverter.Convert(pngGuid, packName, rhcFiles);
			string rhgPath = Path.Combine(rhDir, rhgFile.rhgGuid.ToString()) + ".rhg";
			rhgFile.SerializeToFile(rhgPath);

			RhgSynchronizer rhgSynchronizer = new RhgSynchronizer(cacheFile, rhgFile);
			rhgSynchronizer.Sync();
		}
	}
}
