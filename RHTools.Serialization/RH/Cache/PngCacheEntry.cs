using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class PngCacheEntry : IBinarySerializable
	{
		public RhGuid pngGuid;

		public void Serialize(BinaryWriter writer)
		{
			writer.Write((byte)5);
			writer.Write(pngGuid);
			writer.Write((byte)CacheEntryType.EndOfEntry);
		}

		public static PngCacheEntry Deserialize(BinaryReader reader)
		{
			var entry = new PngCacheEntry();

			reader.ReadByte(); // Always 5
			entry.pngGuid = reader.ReadRhGuid();
			reader.ReadByte(); // Always 255

			return entry;
		}
	}
}
