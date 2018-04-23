using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
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
		public float previewEnd;
		public float unknown1;
		public byte[] unknown2;
		public List<Artist> artists;

		public RhsFile()
		{
			artists = new List<Artist>();
		}

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
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
					case RhsEntryType.PreviewEnd:
						file.previewEnd = reader.ReadSingle();
						break;
					case RhsEntryType.Unknown1:
						file.unknown1 = reader.ReadSingle();
						break;
					case RhsEntryType.Internal:
						file.internalGuid = reader.ReadRhGuid();
						break;
					case RhsEntryType.Artists:
						file.artists.Add(Artist.Deserialize(reader));
						break;
					case RhsEntryType.Unknown2:
						file.unknown2 = reader.ReadBytes(16);
						break;
					default:
						throw new Exception("Unknown song entry type: " + type);
				}
			}

			return file;
		}
	}
}
