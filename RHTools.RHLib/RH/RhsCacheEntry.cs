using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class RhsCacheEntry : IBinarySerializable
	{
		//public CacheEntryType rhsType;
		public RhGuid rhsGuid;
		//public CacheEntryType internalType;
		public RhGuid internalGuid;
		//public CacheEntryType oggType;
		public RhGuid oggGuid;
		//public CacheEntryType pngType;
		public RhGuid pngGuid;
		//public CacheEntryType chartNameType;
		public string chartName;
		public byte[] unknown1;
		public string artist1;
		public byte[] unknown2;
		public string artist2;
		public byte entryEnd; // Always 255?

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static RhsCacheEntry Deserialize(BinaryReader reader)
		{
			RhsCacheEntry entry = new RhsCacheEntry();

			CacheEntryType type;
			while ((type = (CacheEntryType)reader.ReadByte()) != CacheEntryType.EndOfEntry)
			{
				switch (type)
				{
					case CacheEntryType.Rhs:
						entry.rhsGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Internal:
						entry.internalGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Ogg:
						entry.oggGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Png:
						entry.pngGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.ChartName:
						entry.chartName = reader.ReadShortPrefixedString();
						break;
					case CacheEntryType.Unknown3:
						byte[] unknown3Type = reader.ReadBytes(2); // First byte is always 0?
						// Pattern: (unknown3Type[1] + 1) * 12 = num bytes to read
						if (unknown3Type[1] == 1)
							entry.unknown1 = reader.ReadBytes(24);
						else if (unknown3Type[1] == 2)
							entry.unknown1 = reader.ReadBytes(36);
						else if (unknown3Type[1] == 5)
							entry.unknown1 = reader.ReadBytes(72);
						else if (unknown3Type[1] == 9)
							entry.unknown1 = reader.ReadBytes(120);
						else if (unknown3Type[1] == 17)
							entry.unknown1 = reader.ReadBytes(216);
						else if (unknown3Type[1] == 18)
							entry.unknown1 = reader.ReadBytes(228);
						else
							throw new Exception("Unknown data");
						break;
					case CacheEntryType.Artist1:
						entry.artist1 = reader.ReadShortPrefixedString();
						break;
					case CacheEntryType.Empty:
						byte[] empty = reader.ReadBytes(2);
						break;
					case CacheEntryType.Unknown4:
						reader.ReadBytes(14);
						break;
					case CacheEntryType.Unknown6:
						throw new Exception("Unknown data");
					case CacheEntryType.Unknown7:
						throw new Exception("Unknown data");
					case CacheEntryType.Artist2:
						entry.artist2 = reader.ReadShortPrefixedString();
						break;
					default:
						throw new Exception("Unknown data");
				}
			}

			//entry.rhsType = (CacheEntryType)reader.ReadByte();
			//entry.rhsGuid = reader.ReadRhGuid();
			//entry.internalType = (CacheEntryType)reader.ReadByte();
			//entry.internalGuid = reader.ReadRhGuid();
			//entry.oggType = (CacheEntryType)reader.ReadByte();
			//entry.oggGuid = reader.ReadRhGuid();
			//entry.pngType = (CacheEntryType)reader.ReadByte();
			//entry.pngGuid = reader.ReadRhGuid();
			//entry.chartNameType = (CacheEntryType)reader.ReadByte();
			//entry.chartName = reader.ReadShortPrefixedString();
			//entry.unknown1 = reader.ReadBytes(28);
			//if (entry.unknown1[2] == 2)
			//	reader.ReadBytes(12);
			//entry.artist1 = reader.ReadShortPrefixedString();
			//entry.unknown2 = reader.ReadBytes(19);
			//entry.artist2 = reader.ReadShortPrefixedString();
			//entry.entryEnd = reader.ReadByte();

			return entry;
		}
	}
}
