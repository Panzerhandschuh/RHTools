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

		public SmSongToRhprojConverter(string smSongDir, string rhDir, string packName = "Converted")
		{
			this.rhDir = rhDir;

			converter = new SmSongToRhConverter(smSongDir, rhDir, packName);
		}

		public void Convert()
		{
			RhAssets assets = converter.Convert();

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
	}
}
