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
		public int beat;
		public int duration;

		public Note() { }

		public Note(NoteType type, int beat) :
			this(type, beat, 0) { }

		public Note(NoteType type, int beat, int duration)
		{
			this.type = type;
			this.beat = beat;
			this.duration = duration;
		}

		public void Serialize(BinaryWriter writer)
		{
			writer.Write((byte)type);
			writer.Write(beat);
			if (type == NoteType.Hold)
				writer.Write(duration);
		}

		public static Note Deserialize(BinaryReader reader)
		{
			var note = new Note();

			note.type = (NoteType)reader.ReadByte();
			note.beat = reader.ReadInt32();
			if (note.type == NoteType.Hold)
				note.duration = reader.ReadInt32();

			return note;
		}
	}
}
