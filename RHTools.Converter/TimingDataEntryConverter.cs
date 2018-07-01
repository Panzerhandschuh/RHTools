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
		private const int beatMulti = 1000;
		private const int timeMulti = 44100;

		public TimingDataEntry Convert(float beat, float time)
		{
			var entry = new TimingDataEntry();

			entry.beat = (int)(beat * beatMulti);
			entry.time = (long)(time * timeMulti);

			return entry;
		}
	}
}
