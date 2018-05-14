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
		public float offsetConst = 44100f; // Uncertain if this is the exact value, but it matches a common music sampling rate

		public TimingData Convert(SmFile smFile)
		{
			TimingData data = new TimingData();

			data.unknown1 = 0; // Fake value
			data.offsetMultiplier = (long)(-smFile.offset * offsetConst);

			Bpm bpm = smFile.bpms.bpms.SingleOrDefault();
			if (bpm == null)
				throw new NotSupportedException("Simfiles with multiple BPM changes are not currently supported.");

			DisplayBpm displayBpm = smFile.displayBpm;
			//data.entries.Add(new TimingDataEntryConverter().Convert(smFile));
			data.lastEntry = new LastTimingDataEntryConverter().Convert(bpm, displayBpm);

			return data;
		}
	}
}
