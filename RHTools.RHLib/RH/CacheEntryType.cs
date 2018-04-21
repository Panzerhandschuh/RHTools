using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public enum CacheEntryType : byte
	{
		Internal = 0, // Used after RHS guid in RHS entries. It seems to reference some internal guid.
		Rhs = 1,
		Rhc = 2,
		Rhg = 3,
		Ogg = 4,
		Png = 5,
		Unknown1 = 6,
		Unknown2 = 7,
		ChartName = 8, // Also applies to group names
		TimingData = 9,
		Unknown4 = 10,
		Unknown5 = 11,
		Unknown6 = 12,
		DisplayAuthor = 13, // For charts
		DisplayArtist = 14, // For songs
		Artist = 20,
		ArtistName = 33,
		ArtistType = 35,
		EndOfEntry = 255
	}
}
