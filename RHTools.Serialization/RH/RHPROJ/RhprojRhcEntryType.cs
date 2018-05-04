using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public enum RhprojRhcEntryType
	{
		Rhc = 0,
		Rhs = 1,
		Layers = 2,
		SongTitle = 4,
		Unknown1 = 5,
		Internal = 8,
		Artists = 32,
		ArtistName = 33,
		ArtistType = 35,
		EndOfEntry = 255
	}
}
