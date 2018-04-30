using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public enum RhsEntryType
	{
		Rhs = 0,
		Ogg = 1,
		SongTitle = 2,
		TimingData = 3,
		PreviewStart = 4,
		PreviewLength = 5,
		DisplayBpm = 6, // Might be display BPM but usually (always?) deserializes to -1
		Internal = 8, // Might be a guid for online file sharing
		Artists = 32,
		ArtistName = 33,
		ArtistType = 35,
		Png = 64,
		EndOfEntry = 255
	}
}
