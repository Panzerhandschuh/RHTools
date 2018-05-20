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
		public RhsFile Convert(SmFile smFile, RhGuid oggGuid, RhGuid pngGuid, float songOffset)
		{
			RhsFile rhsFile = new RhsFile();

			rhsFile.rhsGuid = RhGuid.NewGuid();
			rhsFile.internalGuid = new RhGuid();
			rhsFile.oggGuid = oggGuid;
			rhsFile.songTitle = smFile.title;
			rhsFile.timingData = new TimingDataConverter().Convert(smFile, songOffset);
			rhsFile.previewStart = smFile.sampleStart;
			rhsFile.previewLength = smFile.sampleLength;
			rhsFile.songLengthOverride = -1f; // TODO: Consider using the beat of the last note (+ some padding)
			rhsFile.pngGuid = pngGuid;
			rhsFile.artists.Add(new Artist(smFile.artist, ArtistType.Artist));

			return rhsFile;
		}
	}
}
