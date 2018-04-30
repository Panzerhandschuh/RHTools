using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class Layer : IBinarySerializable
	{
		// Note: Missing layer property active ranges
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
			writer.Write(unknown1);
			writer.WriteShortPrefixedString(layerName);

			if (isFake)
				writer.Write((byte)LayerEntryType.IsFake);
			if (isEnabled)
				writer.Write((byte)LayerEntryType.IsEnabled);
			if (unknown2 != null)
			{
				writer.Write((byte)LayerEntryType.LayerProperties);
				writer.Write(unknown2);
			}
			writer.Write((byte)LayerEntryType.EndOfEntry);

			writer.Write((int)panelConfig);

			foreach (NoteFlags flags in Enum.GetValues(typeof(NoteFlags)))
				TryWriteNotes(writer, flags);
		}
		
		private void TryWriteNotes(BinaryWriter writer, NoteFlags flags)
		{
			if (panelConfig.HasFlag(flags))
				writer.Write(notes[flags]);
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

			foreach (NoteFlags flags in Enum.GetValues(typeof(NoteFlags)))
				TryReadNotes(reader, layer, flags);

			return layer;
		}
		
		private static void TryReadNotes(BinaryReader reader, Layer layer, NoteFlags flags)
		{
			if (layer.panelConfig.HasFlag(flags))
			{
				List<Note> notes = reader.ReadList(Note.Deserialize);
				layer.notes.Add(flags, notes);
			}
		}
	}
}
