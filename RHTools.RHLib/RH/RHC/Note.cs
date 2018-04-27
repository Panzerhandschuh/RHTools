using RHTools.RHLib.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class Note : IBinarySerializable
	{
		public NoteType type;
		public int startBeat;
		public int duration;

		public void Serialize(BinaryWriter writer)
		{
			writer.Write((byte)type);
			writer.Write(startBeat);
			if (type == NoteType.Hold)
				writer.Write(duration);
		}

		public static Note Deserialize(BinaryReader reader)
		{
			Note note = new Note();

			note.type = (NoteType)reader.ReadByte();
			note.startBeat = reader.ReadInt32();
			if (note.type == NoteType.Hold)
				note.duration = reader.ReadInt32();

			return note;
		}
	}
}
