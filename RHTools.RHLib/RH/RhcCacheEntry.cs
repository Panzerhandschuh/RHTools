using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class RhcCacheEntry : IBinarySerializable
	{
		public RhGuid rhcGuid;
		public RhGuid rhsGuid;
		public RhGuid internalGuid;
		public string chartName;
		public byte[] unknown1;
		public List<Artist> artists;
		public byte[] unknown2;
		public string displayAuthor;

		public RhcCacheEntry()
		{
			artists = new List<Artist>();
		}

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static RhcCacheEntry Deserialize(BinaryReader reader)
		{
			RhcCacheEntry entry = new RhcCacheEntry();

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
					case CacheEntryType.Rhc:
						entry.rhcGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.ChartName:
						entry.chartName = reader.ReadShortPrefixedString();
						break;
					case CacheEntryType.Unknown5:
						entry.unknown1 = reader.ReadBytes(7);
						break;
					case CacheEntryType.Unknown6:
						entry.unknown2 = reader.ReadBytes(36);
						break;
					case CacheEntryType.DisplayAuthor:
						entry.displayAuthor = reader.ReadShortPrefixedString();
						break;
					case CacheEntryType.Artist:
						entry.artists.Add(Artist.Deserialize(reader));
						break;
					default:
						throw new Exception("Unknown song property data type: " + type);
				}
			}

			return entry;
		}
	}
}
