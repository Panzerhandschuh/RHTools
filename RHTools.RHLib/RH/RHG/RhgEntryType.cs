using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public enum RhgEntryType
	{
		Rhg = 0,
		PackName = 1,
		Rhc = 2,
		Png = 3,
		Internal = 8, // Might be a guid for online file sharing
		EndOfEntry = 255
	}
}
