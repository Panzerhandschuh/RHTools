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
		public RhGuid guid;
		public float length;

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static OggCacheEntry Deserialize(BinaryReader reader)
		{
			OggCacheEntry entry = new OggCacheEntry();

			reader.ReadByte(); // Always 4
			entry.guid = reader.ReadRhGuid();
			reader.ReadByte(); // Always 13
			entry.length = reader.ReadSingle();
			reader.ReadByte(); // Always 255

			return entry;
		}
	}
}
