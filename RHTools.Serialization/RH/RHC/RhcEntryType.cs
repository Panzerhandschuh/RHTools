using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public enum RhcEntryType : byte
	{
		Rhc = 0,
		Internal = 8, // Might be a guid for online file sharing
		Rhs = 1,
		Layers = 2,
		ChartName = 4, // Also applies to group names
		Unknown1 = 5,
		Artists = 32,
		ArtistName = 33,
		ArtistType = 35,
		EndOfEntry = 255
	}
}
