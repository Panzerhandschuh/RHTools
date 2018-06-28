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
			var packAssets = new RhPackAssets();

			var files = Directory.GetDirectories(smPackDir);
			foreach (var file in files)
			{
				var songAssets = ConvertSmSongToRh(file);
				packAssets.songAssetList.Add(songAssets);
			}

			packAssets.pngGuid = ConvertPng();
			var rhcGuids = packAssets.songAssetList.SelectMany(x => x.rhcFiles.Select(y => y.rhcGuid));
			packAssets.rhgFile = ConvertRhg(packAssets.pngGuid, rhcGuids);

			return packAssets;
		}

		private RhSongAssets ConvertSmSongToRh(string smSongDir)
		{
			var converter = new SmSongToRhConverter(smSongDir, rhDir, songOffset);
			return converter.Convert();
		}

		private RhGuid ConvertPng()
		{
			var sourcePngPath = Directory.GetFiles(smPackDir, "*.png").FirstOrDefault();
			if (sourcePngPath == null)
				return new RhGuid();

			var pngGuid = RhGuid.NewGuid();
			var destPngPath = Path.Combine(rhDir, pngGuid.ToString()) + ".png";
			File.Copy(sourcePngPath, destPngPath);

			return pngGuid;
		}

		private RhgFile ConvertRhg(RhGuid pngGuid, IEnumerable<RhGuid> rhcGuids)
		{
			var rhgConverter = new RhgConverter();
			var rhgFile = rhgConverter.Convert(pngGuid, packName, rhcGuids);
			var rhgPath = Path.Combine(rhDir, rhgFile.rhgGuid.ToString()) + ".rhg";
			rhgFile.SerializeToFile(rhgPath);

			return rhgFile;
		}
	}
}
