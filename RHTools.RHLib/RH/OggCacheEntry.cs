using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class OggCacheEntry : IBinarySerializable
	{
		public CacheEntryType type;
		public RhGuid guid;
		public byte[] unknown1;
		public byte entryEnd; // Always 255?

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static OggCacheEntry Deserialize(BinaryReader reader)
		{
			OggCacheEntry entry = new OggCacheEntry();

			entry.type = (CacheEntryType)reader.ReadByte();
			entry.guid = reader.ReadRhGuid();
			entry.unknown1 = reader.ReadBytes(5);
			entry.entryEnd = reader.ReadByte();

			return entry;
		}
	}
}
