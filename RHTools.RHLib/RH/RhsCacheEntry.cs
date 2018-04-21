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
		public RhGuid rhsGuid;
		public RhGuid internalGuid;
		public RhGuid oggGuid;
		public RhGuid pngGuid;
		public string chartName;
		public byte[] unknown1;
		public List<string> artists;
		public byte[] unknown2;
		public string displayArtist;

		public RhsCacheEntry()
		{
			artists = new List<string>();
		}

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
					case CacheEntryType.Internal:
						entry.internalGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Rhs:
						entry.rhsGuid = reader.ReadRhGuid();
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
						int numBytes = (unknown3Type[1] + 1) * 12;
						entry.unknown1 = reader.ReadBytes(numBytes);
						break;
					case CacheEntryType.Unknown4:
						reader.ReadBytes(14);
						break;
					case CacheEntryType.Artists:
						entry.artists.Add(reader.ReadShortPrefixedString());
						break;
					case CacheEntryType.Empty:
						byte[] empty = reader.ReadBytes(2); // 0x00FF or 0x01FF? 0 or 1 might indicate Artist vs Contributor
						break;
					case CacheEntryType.ExtraArtistIndicator: // Seems to indicate additional artists (followed by 0x21 / 33)
						break;
					case CacheEntryType.DisplayArtist:
						entry.displayArtist = reader.ReadShortPrefixedString();
						break;
					default:
						throw new Exception("Unknown data");
				}
			}
			
			return entry;
		}
	}
}
