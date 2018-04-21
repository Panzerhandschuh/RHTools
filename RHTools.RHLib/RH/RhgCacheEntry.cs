using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class RhgCacheEntry : IBinarySerializable
	{
		public RhGuid rhgGuid;
		public RhGuid internalGuid;
		public RhGuid pngGuid;
		public string groupName;
		public List<RhGuid> rhcGuids;

		public RhgCacheEntry()
		{
			rhcGuids = new List<RhGuid>();
		}

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static RhgCacheEntry Deserialize(BinaryReader reader)
		{
			RhgCacheEntry entry = new RhgCacheEntry();

			// TODO: Remove duplicate logic for parsing cache entry types
			CacheEntryType type;
			while ((type = (CacheEntryType)reader.ReadByte()) != CacheEntryType.EndOfEntry)
			{
				switch (type)
				{
					case CacheEntryType.Internal:
						entry.internalGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Rhc:
						entry.rhcGuids.Add(reader.ReadRhGuid());
						break;
					case CacheEntryType.Rhg:
						entry.rhgGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Png:
						entry.pngGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.ChartName:
						entry.groupName = reader.ReadShortPrefixedString();
						break;
					default:
						throw new Exception("Unknown song property data type: " + type);
				}
			}

			return entry;
		}
	}
}
