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
			using (Stream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
			using (BinaryWriter writer = new BinaryWriter(stream))
			{
				obj.Serialize(writer);
			}
		}

		public static byte[] SerializeToBytes(this IBinarySerializable obj)
		{
			using (MemoryStream stream = new MemoryStream())
			using (BinaryWriter writer = new BinaryWriter(stream))
			{
				obj.Serialize(writer);
				return stream.ToArray();
			}
		}

		public static T Deserialize<T>(string path, Func<BinaryReader, T> deserializeFunc) where T : IBinarySerializable
		{
			using (Stream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (BinaryReader reader = new BinaryReader(stream))
			{
				return deserializeFunc(reader);
			}
		}
	}
}
