using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class PngCacheEntry : IBinarySerializable
	{
		public CacheEntryType type;
		public RhGuid guid;
		public byte entryEnd; // Always 255?

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static PngCacheEntry Deserialize(BinaryReader reader)
		{
			PngCacheEntry entry = new PngCacheEntry();

			entry.type = (CacheEntryType)reader.ReadByte();
			entry.guid = reader.ReadRhGuid();
			entry.entryEnd = reader.ReadByte();

			return entry;
		}
	}
}
