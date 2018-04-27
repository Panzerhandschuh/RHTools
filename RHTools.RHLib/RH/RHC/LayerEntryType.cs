using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public enum LayerEntryType
	{
		IsFake = 1,
		IsEnabled = 2,
		LayerProperties = 3,
		EndOfEntry = 255
	}
}
