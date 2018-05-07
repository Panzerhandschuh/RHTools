using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class LastTimingDataEntry : IBinarySerializable
	{
		public float bpmMultiplier; // This value / 1024 = actual bpm
		public float displayStartBpm; // Setting this to -1 might hide the display bpm
		public float displayEndBpm; // Setting this to -1 might hide the display bpm

		public void Serialize(BinaryWriter writer)
		{
			writer.Write(bpmMultiplier);
			writer.Write(displayStartBpm);
			writer.Write(displayEndBpm);
		}

		public static LastTimingDataEntry Deserialize(BinaryReader reader)
		{
			LastTimingDataEntry entry = new LastTimingDataEntry();

			entry.bpmMultiplier = reader.ReadSingle();
			entry.displayStartBpm = reader.ReadSingle();
			entry.displayEndBpm = reader.ReadSingle();

			return entry;
		}
	}
}
