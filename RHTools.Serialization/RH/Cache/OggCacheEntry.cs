using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class OggCacheEntry : IBinarySerializable
	{
		public RhGuid oggGuid;
		public float length;

		public void Serialize(BinaryWriter writer)
		{
			writer.Write((byte)4);
			writer.Write(oggGuid);
			writer.Write((byte)13);
			writer.Write(length);
			writer.Write((byte)CacheEntryType.EndOfEntry);
		}

		public static OggCacheEntry Deserialize(BinaryReader reader)
		{
			var entry = new OggCacheEntry();

			reader.ReadByte(); // Always 4
			entry.oggGuid = reader.ReadRhGuid();
			reader.ReadByte(); // Always 13
			entry.length = reader.ReadSingle();
			reader.ReadByte(); // Always 255

			return entry;
		}
	}
}
