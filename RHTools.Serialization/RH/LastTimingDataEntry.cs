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
		public float displayMinBpm; // Setting this to -1 might hide the display bpm
		public float displayMaxBpm; // Setting this to -1 might hide the display bpm

		public void Serialize(BinaryWriter writer)
		{
			writer.Write(bpmMultiplier);
			writer.Write(displayMinBpm);
			writer.Write(displayMaxBpm);
		}

		public static LastTimingDataEntry Deserialize(BinaryReader reader)
		{
			var entry = new LastTimingDataEntry();

			entry.bpmMultiplier = reader.ReadSingle();
			entry.displayMinBpm = reader.ReadSingle();
			entry.displayMaxBpm = reader.ReadSingle();

			return entry;
		}
	}
}
