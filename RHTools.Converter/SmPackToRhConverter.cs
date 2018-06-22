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
	public class SmPackToRhConverter
	{
		private string smPackDir;
		private string rhDir;
		private string packName;
		private float songOffset;

		public SmPackToRhConverter(string smPackDir, string rhDir, string packName, float songOffset)
		{
			this.smPackDir = smPackDir;
			this.rhDir = rhDir;
			this.packName = packName;
			this.songOffset = songOffset;
		}

		public RhPackAssets Convert()
		{
			RhPackAssets packAssets = new RhPackAssets();

			string[] files = Directory.GetDirectories(smPackDir);
			foreach (string file in files)
			{
				RhSongAssets songAssets = ConvertSmSongToRh(file);
				packAssets.songAssetList.Add(songAssets);
			}

			packAssets.pngGuid = ConvertPng();
			IEnumerable<RhGuid> rhcGuids = packAssets.songAssetList.SelectMany(x => x.rhcFiles.Select(y => y.rhcGuid));
			packAssets.rhgFile = ConvertRhg(packAssets.pngGuid, rhcGuids);

			return packAssets;
		}

		private RhSongAssets ConvertSmSongToRh(string smSongDir)
		{
			SmSongToRhConverter converter = new SmSongToRhConverter(smSongDir, rhDir, songOffset);
			return converter.Convert();
		}

		private RhGuid ConvertPng()
		{
			string sourcePngPath = Directory.GetFiles(smPackDir, "*.png").FirstOrDefault();
			if (sourcePngPath == null)
				return new RhGuid();

			RhGuid pngGuid = RhGuid.NewGuid();
			string destPngPath = Path.Combine(rhDir, pngGuid.ToString()) + ".png";
			File.Copy(sourcePngPath, destPngPath);

			return pngGuid;
		}

		private RhgFile ConvertRhg(RhGuid pngGuid, IEnumerable<RhGuid> rhcGuids)
		{
			RhgConverter rhgConverter = new RhgConverter();
			RhgFile rhgFile = rhgConverter.Convert(pngGuid, packName, rhcGuids);
			string rhgPath = Path.Combine(rhDir, rhgFile.rhgGuid.ToString()) + ".rhg";
			rhgFile.SerializeToFile(rhgPath);

			return rhgFile;
		}
	}
}
