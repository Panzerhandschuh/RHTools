using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class Layer : IBinarySerializable
	{
		public byte[] unknown1;
		public string layerName;
		public bool isEnabled;
		public bool isFake;
		public byte[] unknown2;
		public NoteFlags panelConfig;
		public Dictionary<NoteFlags, List<Note>> notes;

		public Layer()
		{
			notes = new Dictionary<NoteFlags, List<Note>>();
		}

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static Layer Deserialize(BinaryReader reader)
		{
			Layer layer = new Layer();

			layer.unknown1 = reader.ReadBytes(2);
			layer.layerName = reader.ReadShortPrefixedString();

			LayerEntryType type;
			while ((type = (LayerEntryType)reader.ReadByte()) != LayerEntryType.EndOfEntry)
			{
				switch (type)
				{
					case LayerEntryType.IsFake:
						layer.isFake = true;
						break;
					case LayerEntryType.IsEnabled:
						layer.isEnabled = true;
						break;
					case LayerEntryType.LayerProperties:
						layer.unknown2 = reader.ReadBytes(12);
						break;
					default:
						throw new Exception("Unknown layer entry type: " + type);
				}
			}

			layer.panelConfig = (NoteFlags)reader.ReadInt32();

			TryReadPad1Notes(reader, layer);
			TryReadPad2Notes(reader, layer);

			return layer;
		}

		private static void TryReadPad1Notes(BinaryReader reader, Layer layer)
		{
			TryReadNotes(reader, layer, NoteFlags.Pad1DownLeft);
			TryReadNotes(reader, layer, NoteFlags.Pad1Left);
			TryReadNotes(reader, layer, NoteFlags.Pad1UpLeft);
			TryReadNotes(reader, layer, NoteFlags.Pad1Down);
			TryReadNotes(reader, layer, NoteFlags.Pad1Center);
			TryReadNotes(reader, layer, NoteFlags.Pad1Up);
			TryReadNotes(reader, layer, NoteFlags.Pad1UpRight);
			TryReadNotes(reader, layer, NoteFlags.Pad1Right);
			TryReadNotes(reader, layer, NoteFlags.Pad1DownRight);
		}

		private static void TryReadPad2Notes(BinaryReader reader, Layer layer)
		{
			TryReadNotes(reader, layer, NoteFlags.Pad2DownLeft);
			TryReadNotes(reader, layer, NoteFlags.Pad2Left);
			TryReadNotes(reader, layer, NoteFlags.Pad2UpLeft);
			TryReadNotes(reader, layer, NoteFlags.Pad2Down);
			TryReadNotes(reader, layer, NoteFlags.Pad2Center);
			TryReadNotes(reader, layer, NoteFlags.Pad2Up);
			TryReadNotes(reader, layer, NoteFlags.Pad2UpRight);
			TryReadNotes(reader, layer, NoteFlags.Pad2Right);
			TryReadNotes(reader, layer, NoteFlags.Pad2DownRight);
		}

		private static void TryReadNotes(BinaryReader reader, Layer layer, NoteFlags flags)
		{
			if (layer.panelConfig.HasFlag(flags))
			{
				List<Note> notes = ReadNotes(reader);
				layer.notes.Add(flags, notes);
			}
		}

		private static List<Note> ReadNotes(BinaryReader reader)
		{
			List<Note> beats = new List<Note>();

			int numNotes = reader.ReadInt32();
			for (int i = 0; i < numNotes; i++)
				beats.Add(Note.Deserialize(reader));

			return beats;
		}
	}
}
