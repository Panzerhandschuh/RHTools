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

		public TimingData Convert(SmFile smFile, float songOffset)
		{
			TimingData data = new TimingData();

			data.unknown1 = 0; // Fake value
			data.offsetMultiplier = (long)(-(smFile.offset + songOffset) * offsetConst);

            for (int i = 0; i < smFile.bpms.bpms.Count - 1; i++)
            {
                var bpm = smFile.bpms.bpms[i];
                data.entries.Add(new TimingDataEntryConverter().Convert(smFile)); // TODO: Add real conversion
            }

            var lastBpm = smFile.bpms.bpms.LastOrDefault();
			DisplayBpm displayBpm = smFile.displayBpm;
            data.lastEntry = new LastTimingDataEntryConverter().Convert(lastBpm, displayBpm);

			return data;
		}
	}
}
