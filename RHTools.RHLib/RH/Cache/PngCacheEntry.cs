using RHTools.RHLib.Serialization;
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
		public RhGuid guid;

		public void Serialize(BinaryWriter writer)
		{
			writer.Write((byte)5);
			writer.Write(guid);
			writer.Write((byte)CacheEntryType.EndOfEntry);
		}

		public static PngCacheEntry Deserialize(BinaryReader reader)
		{
			PngCacheEntry entry = new PngCacheEntry();

			reader.ReadByte(); // Always 5
			entry.guid = reader.ReadRhGuid();
			reader.ReadByte(); // Always 255

			return entry;
		}
	}
}
