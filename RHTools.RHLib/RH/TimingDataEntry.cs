using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class TimingDataEntry : IBinarySerializable
	{
		public int beat; // Uncertain
		public float startBpm; // Uncertain
		public float endBpm; // Uncertain

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
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
