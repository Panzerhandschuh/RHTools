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
		public bool isHold;
		public int startBeat;
		public int duration;

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static Note Deserialize(BinaryReader reader)
		{
			Note note = new Note();

			note.isHold = reader.ReadBoolean();
			note.startBeat = reader.ReadInt32();
			if (note.isHold)
				note.duration = reader.ReadInt32();

			return note;
		}
	}
}
