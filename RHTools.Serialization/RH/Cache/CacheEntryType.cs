using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public enum CacheEntryType : byte
	{
		Internal = 0, // Might be a guid for online file sharing
		Rhs = 1,
		Rhc = 2,
		Rhg = 3,
		Ogg = 4,
		Png = 5,
		ChartName = 8, // Also applies to song titles and pack names
		TimingData = 9,
		SongLengthOverride = 10,
		PreviewStart = 11, // This might have a different meaning for RHC entries
		PreviewLength = 12, // This might have a different meaning for RHC entries
		DisplayAuthor = 13, // For charts
		DisplayArtist = 14, // For songs
		Artists = 20,
		ArtistName = 33,
		ArtistType = 35,
		EndOfEntry = 255
	}
}
