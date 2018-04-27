using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.Serialization
{
	public static class IBinarySerializableExtensions
	{
		public static byte[] SerializeToBytes(this IBinarySerializable obj)
		{
			using (MemoryStream stream = new MemoryStream())
			using (BinaryWriter writer = new BinaryWriter(stream))
			{
				obj.Serialize(writer);
				return stream.ToArray();
			}
		}
	}
}
