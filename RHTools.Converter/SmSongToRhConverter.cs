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
		private float songOffset;
		private SmFile smFile;

		public SmSongToRhConverter(string smSongDir, string rhDir, float songOffset = 0f)
		{
			this.smSongDir = smSongDir;
			this.rhDir = rhDir;
			this.songOffset = songOffset;

			string smFilePath = Directory.GetFiles(smSongDir, "*.sm").Single();
			smFile = ITextSerializableExtensions.Deserialize(smFilePath, SmFile.Deserialize);
		}

		// TODO: Consider making all converters override a Convert<T>() method
		public RhSongAssets Convert()
		{
			RhSongAssets assets = new RhSongAssets();

			assets.oggGuid = ConvertOgg();
			// TODO: Force pngs to power of 2 dimensions
			assets.pngGuid = new RhGuid();//ConvertPng();
			assets.rhsFile = ConvertRhs(assets.oggGuid, assets.pngGuid);
			assets.rhcFiles = ConvertRhc(assets.rhsFile.rhsGuid);

			return assets;
		}

		private RhGuid ConvertOgg()
		{
			RhGuid oggGuid = RhGuid.NewGuid();
			string sourceOggPath = Path.Combine(smSongDir, smFile.music);
			string destOggPath = Path.Combine(rhDir, oggGuid.ToString()) + ".ogg";
			File.Copy(sourceOggPath, destOggPath);

			return oggGuid;
		}

		private RhGuid ConvertPng()
		{
			string sourcePngPath = Path.Combine(smSongDir, smFile.background);

			RhGuid pngGuid = RhGuid.NewGuid();
			string destPngPath = Path.Combine(rhDir, pngGuid.ToString()) + ".png";
			File.Copy(sourcePngPath, destPngPath);

			return pngGuid;
		}

		private RhsFile ConvertRhs(RhGuid oggGuid, RhGuid pngGuid)
		{
			RhsConverter rhsConverter = new RhsConverter();
			RhsFile rhsFile = rhsConverter.Convert(smFile, oggGuid, pngGuid, songOffset);
			string rhsPath = Path.Combine(rhDir, rhsFile.rhsGuid.ToString()) + ".rhs";
			rhsFile.SerializeToFile(rhsPath);

			return rhsFile;
		}

		private List<RhcFile> ConvertRhc(RhGuid rhsGuid)
		{
			RhcConverter rhcConverter = new RhcConverter();
			List<RhcFile> rhcFiles = rhcConverter.Convert(smFile, rhsGuid, smFile.credit);
			foreach (RhcFile rhcFile in rhcFiles)
			{
				string rhcPath = Path.Combine(rhDir, rhcFile.rhcGuid.ToString()) + ".rhc";
				rhcFile.SerializeToFile(rhcPath);
			}

			return rhcFiles;
		}
	}
}
