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
		public List<RhcFile> Convert(SmFile smFile)
		{
			List<RhcFile> rhcFiles = new List<RhcFile>();

			foreach (Chart chart in smFile.charts)
				rhcFiles.Add(ConvertRhcFile(chart));

			return rhcFiles;
		}

		private RhcFile ConvertRhcFile(Chart chart)
		{
			RhcFile rhcFile = new RhcFile();

			rhcFile.rhcGuid = RhGuid.NewGuid();
			rhcFile.internalGuid = new RhGuid();
			//rhcFile.rhsGuid = ;
			rhcFile.chartName = chart.difficulty;
			//rhcFile.unknown1 = ;
			//rhcFile.artists.Add(new Artist()); // Use smFile.credit
			rhcFile.layers = new LayersConverter().Convert(chart);

			return rhcFile;
		}
	}
}
