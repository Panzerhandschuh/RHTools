using RHTools.Serialization.RH;
using RHTools.Serialization.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public class RhcConverter
	{
		public List<RhcFile> Convert(SmFile smFile, RhGuid rhsGuid, string artist)
		{
			List<RhcFile> rhcFiles = new List<RhcFile>();

			foreach (Chart chart in smFile.charts)
				rhcFiles.Add(ConvertRhcFile(chart, rhsGuid, artist));

			return rhcFiles;
		}

		private RhcFile ConvertRhcFile(Chart chart, RhGuid rhsGuid, string artist)
		{
			RhcFile rhcFile = new RhcFile();

			rhcFile.rhcGuid = RhGuid.NewGuid();
			rhcFile.internalGuid = new RhGuid();
			rhcFile.rhsGuid = rhsGuid;
			rhcFile.chartName = chart.difficulty;
			rhcFile.unknown1 = 0; // Fake value
			rhcFile.artists.Add(new Artist(artist, ArtistType.Artist));
			rhcFile.layers = new LayersConverter().Convert(chart);

			return rhcFile;
		}
	}
}
