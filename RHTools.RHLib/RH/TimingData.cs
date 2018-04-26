﻿using RHTools.RHLib.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class TimingData : IBinarySerializable
	{
		//public long position;
		public int unknown1;
		public int unknown2;
		public TimingDataEntry[] entries;

		public void Serialize(BinaryWriter writer)
		{
			writer.Write((byte)0); // Unknown. Always 0?
			writer.Write(entries.Length);
			writer.Write(unknown1);
			writer.Write(unknown2);
			writer.Write(entries, false);
		}

		public static TimingData Deserialize(BinaryReader reader)
		{
			TimingData data = new TimingData();

			//data.position = reader.BaseStream.Position;
			reader.ReadByte(); // Always 0?
			int numEntries = reader.ReadInt32();
			data.unknown1 = reader.ReadInt32();
			data.unknown2 = reader.ReadInt32();
			data.entries = reader.ReadArray(TimingDataEntry.Deserialize, numEntries);

			return data;
		}
	}
}
