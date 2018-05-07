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
		public int beat;
		public float startBpm;
		public float endBpm;

		public void Serialize(BinaryWriter writer)
		{
			writer.Write(beat);
			writer.Write(startBpm);
			writer.Write(endBpm);
		}

		public static TimingDataEntry Deserialize(BinaryReader reader)
		{
			TimingDataEntry entry = new TimingDataEntry();

			entry.beat = reader.ReadInt32();
			entry.startBpm = reader.ReadSingle();
			entry.endBpm = reader.ReadSingle();

			return entry;
		}
	}
}
