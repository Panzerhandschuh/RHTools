using RHTools.Serialization.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class RhsFile : IBinarySerializable
	{
		public byte version; // Uncertain, but most likely a version number
		public RhGuid rhsGuid;
		public RhGuid internalGuid;
		public RhGuid oggGuid;
		public string songTitle;
		public TimingData timingData;
		public float previewStart;
		public float previewLength;
		public float displayBpm; // Uncertain. Always -1?
		public byte[] unknown2;
		public List<Artist> artists;

		public RhsFile()
		{
			artists = new List<Artist>();
		}

		public void Serialize(BinaryWriter writer)
		{
			writer.Write(version);

			writer.WriteOptionalData((byte)RhsEntryType.Rhs, rhsGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)RhsEntryType.Internal, internalGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)RhsEntryType.Ogg, oggGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)RhsEntryType.SongTitle, songTitle, (x) => writer.WriteShortPrefixedString(x));
			writer.WriteOptionalData((byte)RhsEntryType.TimingData, timingData, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)RhsEntryType.PreviewStart, previewStart, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)RhsEntryType.PreviewLength, previewLength, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)RhsEntryType.DisplayBpm, displayBpm, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)RhsEntryType.Unknown2, unknown2, (x) => writer.Write(x));
			writer.WritePrefixedList((byte)RhsEntryType.Artists, artists, (x) => writer.Write(x));
			writer.Write((byte)RhsEntryType.EndOfEntry);
		}

		public static RhsFile Deserialize(BinaryReader reader)
		{
			RhsFile file = new RhsFile();

			file.version = reader.ReadByte();

			RhsEntryType type;
			while ((type = (RhsEntryType)reader.ReadByte()) != RhsEntryType.EndOfEntry)
			{
				switch (type)
				{
					case RhsEntryType.Rhs:
						file.rhsGuid = reader.ReadRhGuid();
						break;
					case RhsEntryType.Internal:
						file.internalGuid = reader.ReadRhGuid();
						break;
					case RhsEntryType.Ogg:
						file.oggGuid = reader.ReadRhGuid();
						break;
					case RhsEntryType.SongTitle:
						file.songTitle = reader.ReadShortPrefixedString();
						break;
					case RhsEntryType.TimingData:
						file.timingData = TimingData.Deserialize(reader);
						break;
					case RhsEntryType.PreviewStart:
						file.previewStart = reader.ReadSingle();
						break;
					case RhsEntryType.PreviewLength:
						file.previewLength = reader.ReadSingle();
						break;
					case RhsEntryType.DisplayBpm:
						file.displayBpm = reader.ReadSingle();
						break;
					case RhsEntryType.Unknown2:
						file.unknown2 = reader.ReadBytes(16);
						break;
					case RhsEntryType.Artists:
						file.artists.Add(Artist.Deserialize(reader));
						break;
					default:
						throw new Exception("Unknown song entry type: " + type);
				}
			}

			return file;
		}
	}
}
