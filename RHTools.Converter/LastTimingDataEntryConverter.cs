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
		private const int bpmConst = 1024;

		public LastTimingDataEntry Convert(float bpm, DisplayBpm displayBpm)
		{
			var entry = new LastTimingDataEntry();

			entry.bpmMultiplier = bpm * bpmConst;
			if (displayBpm != null)
			{
				entry.displayMinBpm = displayBpm.minBpm;
				entry.displayMaxBpm = displayBpm.maxBpm;
			}
			else
			{
				entry.displayMinBpm = -1f;
				entry.displayMaxBpm = -1f;
			}

			return entry;
		}
	}
}
