using RHTools.Serialization.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
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
			writer.WriteOptionalData((byte)CacheEntryType.Rhc, rhcGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.Rhs, rhsGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.Internal, internalGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.ChartName, chartName, (x) => writer.WriteShortPrefixedString(x));
			writer.WriteOptionalData((byte)CacheEntryType.PreviewStart, unknown1, (x) => writer.Write(x));
			writer.WritePrefixedList((byte)CacheEntryType.Artists, artists, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.PreviewLength, unknown2, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.DisplayAuthor, displayAuthor, (x) => writer.WriteShortPrefixedString(x));
			writer.Write((byte)CacheEntryType.EndOfEntry);
		}

		public static RhcCacheEntry Deserialize(BinaryReader reader)
		{
			RhcCacheEntry entry = new RhcCacheEntry();

			// TODO: Remove duplicate logic for parsing cache entry types
			CacheEntryType type;
			while ((type = (CacheEntryType)reader.ReadByte()) != CacheEntryType.EndOfEntry)
			{
				switch (type)
				{
					case CacheEntryType.Rhc:
						entry.rhcGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Rhs:
						entry.rhsGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Internal:
						entry.internalGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.ChartName:
						entry.chartName = reader.ReadShortPrefixedString();
						break;
					case CacheEntryType.PreviewStart:
						entry.unknown1 = reader.ReadBytes(7);
						break;
					case CacheEntryType.Artists:
						entry.artists.Add(Artist.Deserialize(reader));
						break;
					case CacheEntryType.PreviewLength:
						entry.unknown2 = reader.ReadBytes(36);
						break;
					case CacheEntryType.DisplayAuthor:
						entry.displayAuthor = reader.ReadShortPrefixedString();
						break;
					default:
						throw new Exception("Unknown cache entry type: " + type);
				}
			}

			return entry;
		}
	}
}
