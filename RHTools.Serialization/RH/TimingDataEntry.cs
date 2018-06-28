using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class TimingDataEntry : IBinarySerializable
	{
		public int beat; // This value / 1000 = beat
        public long time; // This value / 44100 = time

		public void Serialize(BinaryWriter writer)
		{
			writer.Write(beat);
			writer.Write(time);
		}

		public static TimingDataEntry Deserialize(BinaryReader reader)
		{
			var entry = new TimingDataEntry();

			entry.beat = reader.ReadInt32();
            entry.time = reader.ReadInt64();

			return entry;
		}
	}
}
