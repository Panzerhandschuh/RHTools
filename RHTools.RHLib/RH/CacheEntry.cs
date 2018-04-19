using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class CacheEntry : IBinarySerializable
	{
		public CacheType type;
		public RHGuid guid;
		public byte[] unknown1;

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static CacheEntry Deserialize(BinaryReader reader)
		{
			CacheEntry entry = new CacheEntry();

			entry.type = (CacheType)reader.ReadByte();
			entry.guid = reader.ReadRHGuid();
			entry.unknown1 = reader.ReadBytes(6);

			return entry;
		}
	}
}
