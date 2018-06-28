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
			var entry = new TimingDataEntry();

			entry.beat = 4000; // Fake value
			entry.time = 0; // Fake value

			return entry;
		}
	}
}
