using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class RhgCacheEntry : IBinarySerializable
	{
		public RhGuid rhgGuid;
		public RhGuid internalGuid;
		public RhGuid pngGuid;
		public string packName;
		public List<RhGuid> rhcGuids;

		public RhgCacheEntry()
		{
			rhcGuids = new List<RhGuid>();
		}

		public void Serialize(BinaryWriter writer)
		{
			writer.WriteOptionalData((byte)CacheEntryType.Rhg, rhgGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.Internal, internalGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.Png, pngGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.ChartName, packName, (x) => writer.WriteShortPrefixedString(x));
			writer.WritePrefixedList((byte)CacheEntryType.Rhc, rhcGuids, (x) => writer.Write(x));
			writer.Write((byte)CacheEntryType.EndOfEntry);
		}

		public static RhgCacheEntry Deserialize(BinaryReader reader)
		{
			var entry = new RhgCacheEntry();

			// TODO: Remove duplicate logic for parsing cache entry types
			CacheEntryType type;
			while ((type = (CacheEntryType)reader.ReadByte()) != CacheEntryType.EndOfEntry)
			{
				switch (type)
				{
					case CacheEntryType.Rhg:
						entry.rhgGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Internal:
						entry.internalGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Png:
						entry.pngGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.ChartName:
						entry.packName = reader.ReadShortPrefixedString();
						break;
					case CacheEntryType.Rhc:
						entry.rhcGuids.Add(reader.ReadRhGuid());
						break;
					default:
						throw new Exception("Unknown cache entry type: " + type);
				}
			}

			return entry;
		}
	}
}
