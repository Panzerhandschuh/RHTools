﻿using System;
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
		public byte[] unknown2;
		public NoteFlags panelConfig;
		public Dictionary<NoteFlags, List<int>> notes;

		public Layer()
		{
			notes = new Dictionary<NoteFlags, List<int>>();
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
			layer.unknown2 = reader.ReadBytes(15);
			layer.panelConfig = (NoteFlags)reader.ReadInt32();

			TryReadNotes(reader, layer, NoteFlags.DownLeft);
			TryReadNotes(reader, layer, NoteFlags.Left);
			TryReadNotes(reader, layer, NoteFlags.UpLeft);
			TryReadNotes(reader, layer, NoteFlags.Down);
			TryReadNotes(reader, layer, NoteFlags.Center);
			TryReadNotes(reader, layer, NoteFlags.Up);
			TryReadNotes(reader, layer, NoteFlags.UpRight);
			TryReadNotes(reader, layer, NoteFlags.Right);
			TryReadNotes(reader, layer, NoteFlags.DownRight);

			return layer;
		}

		private static void TryReadNotes(BinaryReader reader, Layer layer, NoteFlags flags)
		{
			if (layer.panelConfig.HasFlag(flags))
			{
				List<int> beats = ReadNotes(reader);
				layer.notes.Add(flags, beats);
			}
		}

		private static List<int> ReadNotes(BinaryReader reader)
		{
			List<int> beats = new List<int>();

			int numNotes = reader.ReadInt32();
			for (int i = 0; i < numNotes; i++)
			{
				reader.ReadByte();
				beats.Add(reader.ReadInt32());
			}

			return beats;
		}
	}
}
