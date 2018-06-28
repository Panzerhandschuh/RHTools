using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization
{
	public static class IBinarySerializableExtensions
	{
		public static void SerializeToFile(this IBinarySerializable obj, string path)
		{
			using (Stream stream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Write))
			using (var writer = new BinaryWriter(stream))
			{
				obj.Serialize(writer);
			}
		}

		public static byte[] SerializeToBytes(this IBinarySerializable obj)
		{
			using (var stream = new MemoryStream())
			using (var writer = new BinaryWriter(stream))
			{
				obj.Serialize(writer);
				return stream.ToArray();
			}
		}

		public static T Deserialize<T>(string path, Func<BinaryReader, T> deserializeFunc) where T : IBinarySerializable
		{
			using (Stream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (var reader = new BinaryReader(stream))
			{
				return deserializeFunc(reader);
			}
		}
	}
}
