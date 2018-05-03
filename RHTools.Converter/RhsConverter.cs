using RHTools.Serialization.RH;
using RHTools.Serialization.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public class RhsConverter
	{
		public RhsFile Convert(SmFile smFile, RhGuid oggGuid, RhGuid pngGuid)
		{
			RhsFile rhsFile = new RhsFile();

			rhsFile.rhsGuid = RhGuid.NewGuid();
			rhsFile.internalGuid = new RhGuid();
			rhsFile.oggGuid = oggGuid;
			rhsFile.songTitle = smFile.title;
			rhsFile.timingData = new TimingDataConverter().Convert(smFile);
			rhsFile.previewStart = smFile.sampleStart;
			rhsFile.previewLength = smFile.sampleLength;
			rhsFile.unknown1 = -1f; // Always set to -1 or 177 in existing game files
			rhsFile.pngGuid = pngGuid;
			rhsFile.artists.Add(new Artist(smFile.artist, ArtistType.Artist));

			return rhsFile;
		}
	}
}
