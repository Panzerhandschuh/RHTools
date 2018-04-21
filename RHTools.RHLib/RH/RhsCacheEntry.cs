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
		public List<Artist> artists;
		public string displayArtist;

		public RhsCacheEntry()
		{
			artists = new List<Artist>();
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
						int numBytes = ((unknown3Type[1] + 1) * 12) - 1;
						entry.unknown1 = reader.ReadBytes(numBytes);
						break;
					case CacheEntryType.Unknown4:
						reader.ReadBytes(14);
						break;
					case CacheEntryType.Artist:
						entry.artists.Add(Artist.Deserialize(reader));
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
