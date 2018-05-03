using RHTools.Serialization.RH;
using RHTools.Serialization.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public class TimingDataEntryConverter
	{
		public TimingDataEntry Convert(SmFile smFile)
		{
			TimingDataEntry entry = new TimingDataEntry();

			entry.beat = 1216151552; // Fake value
			entry.startBpm = 253f; // Fake value
			entry.endBpm = 253f; // Fake value

			return entry;
		}
	}
}
