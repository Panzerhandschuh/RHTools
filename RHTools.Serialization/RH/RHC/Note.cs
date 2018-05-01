using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class Note : IBinarySerializable
	{
		public NoteType type;
		public int startBeat;
		public int duration;

		public Note() { }

		public Note(NoteType type, int startBeat) :
			this(type, startBeat, 0) { }

		public Note(NoteType type, int startBeat, int duration)
		{
			this.type = type;
			this.startBeat = startBeat;
			this.duration = duration;
		}

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
