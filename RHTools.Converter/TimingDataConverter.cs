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
			//data.entries.Add(new TimingDataEntryConverter().Convert(smFile));
			Bpm bpm = smFile.bpms.bpms.First();
			DisplayBpm displayBpm = smFile.displayBpm;
			data.lastEntry = new LastTimingDataEntryConverter().Convert(bpm, displayBpm);

			return data;
		}
	}
}
