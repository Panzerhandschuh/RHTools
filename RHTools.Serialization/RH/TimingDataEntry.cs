﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class TimingDataEntry : IBinarySerializable
	{
		//public long position;
		public int beat; // Uncertain
		public float startBpm; // Uncertain
		public float endBpm; // Uncertain
		//public BpmChange bpmChange;

		public void Serialize(BinaryWriter writer)
		{
			writer.Write(beat);
			writer.Write(startBpm);
			writer.Write(endBpm);
		}

		public static TimingDataEntry Deserialize(BinaryReader reader)
		{
			TimingDataEntry entry = new TimingDataEntry();

			//entry.position = reader.BaseStream.Position;
			entry.beat = reader.ReadInt32();
			entry.startBpm = reader.ReadSingle();
			entry.endBpm = reader.ReadSingle();

			return entry;
		}
	}
}