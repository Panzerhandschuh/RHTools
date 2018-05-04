using RHTools.Serialization.RH;
using RHTools.Serialization.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public class TimingDataConverter
	{
		public TimingData Convert(SmFile smFile)
		{
			TimingData data = new TimingData();

			data.unknown1 = 0; // Fake value
			data.offsetMultiplier = 0; // Fake value
			data.entries.Add(new TimingDataEntryConverter().Convert(smFile));

			return data;
		}
	}
}
