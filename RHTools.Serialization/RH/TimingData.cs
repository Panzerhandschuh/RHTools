using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class TimingData : IBinarySerializable
	{
		public byte unknown1; // Always 0?
		public long offsetMultiplier; // This value * some constant = offset. I'm not sure where the constant comes from. Maybe it comes from the song's sample rate. HeHeHe has a constant of ~0.0000226757.
		public List<TimingDataEntry> entries;
		public LastTimingDataEntry lastEntry;

		public TimingData()
		{
			entries = new List<TimingDataEntry>();
		}

		public void Serialize(BinaryWriter writer)
		{
			writer.Write(unknown1); // Unknown. Always 0?
			writer.Write(entries.Count + 1);
			writer.Write(offsetMultiplier);
			writer.Write(entries, false);
			writer.Write(lastEntry);
		}

		public static TimingData Deserialize(BinaryReader reader)
		{
			TimingData data = new TimingData();

			data.unknown1 = reader.ReadByte(); // Always 0?
			int numEntries = reader.ReadInt32();
			data.offsetMultiplier = reader.ReadInt64();
			data.entries = reader.ReadList(TimingDataEntry.Deserialize, numEntries - 1);
			data.lastEntry = LastTimingDataEntry.Deserialize(reader);

			return data;
		}
	}
}
