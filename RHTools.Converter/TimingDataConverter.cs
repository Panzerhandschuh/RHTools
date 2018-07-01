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
			var data = new TimingData();

			data.unknown1 = 0; // Fake value
			var offset = -(smFile.offset + songOffset); // Might need to negate this
			data.offsetMultiplier = (long)(offset * offsetConst);

			data.entries = ConvertBpms(smFile, offset);
			data.lastEntry = ConvertLastBpm(smFile);

			return data;
		}

		/// <summary>
		/// Converts all BPMs except for the last BPM
		/// </summary>
		private static List<TimingDataEntry> ConvertBpms(SmFile smFile, float offset)
		{
			var entries = new List<TimingDataEntry>();

			var prevTime = offset;
			for (var i = 1; i < smFile.bpms.bpms.Count; i++)
			{
				var prevBpm = smFile.bpms.bpms[i - 1];
				var curBpm = smFile.bpms.bpms[i];

				var curTime = 60 * ((curBpm.beat - prevBpm.beat) / prevBpm.bpm) + prevTime;
				entries.Add(new TimingDataEntryConverter().Convert(curBpm.beat, curTime));

				prevTime = curTime;
			}

			return entries;
		}

		private static LastTimingDataEntry ConvertLastBpm(SmFile smFile)
		{
			var lastBpm = smFile.bpms.bpms.LastOrDefault();
			var displayBpm = smFile.displayBpm;
			return new LastTimingDataEntryConverter().Convert(lastBpm.bpm, displayBpm);
		}
	}
}
