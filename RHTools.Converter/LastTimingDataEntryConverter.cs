using RHTools.Serialization.RH;
using RHTools.Serialization.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public class LastTimingDataEntryConverter
	{
		public LastTimingDataEntry Convert(SmFile smFile)
		{
			LastTimingDataEntry entry = new LastTimingDataEntry();

			entry.bpmMultiplier = 142335.8f; // Fake value
			entry.displayStartBpm = -1; // Fake value
			entry.displayEndBpm = -1; // Fake value

			return entry;
		}
	}
}
