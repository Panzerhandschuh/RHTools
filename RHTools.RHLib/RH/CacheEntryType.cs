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
		ChartName = 8,
		Unknown3 = 9, // Used after chart names in RHS entries
		Unknown4 = 10, // 17 bytes. Used after the empty entry
		Unknown5 = 11, // 9 bytes
		Unknown6 = 12, // ? bytes. Used after the empty entry
		Artist2 = 14,
		Unknown7 = 20, // 17 bytes. Used after the empty entry
		Artist1 = 33,
		Empty = 35, // 2 bytes? Used after artist1 in RHS entries. Seems to always be followed by 0x00FF
		EndOfEntry = 255
	}
}
