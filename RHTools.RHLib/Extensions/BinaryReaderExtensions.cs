using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization
{
	public static class BinaryReaderExtensions
	{
		public static RhGuid ReadRhGuid(this BinaryReader reader)
		{
			byte[] bytes = reader.ReadBytes(16);
			return new RhGuid(bytes);
		}

		public static string ReadShortPrefixedString(this BinaryReader reader)
		{
			ushort length = reader.ReadUInt16();
			char[] chars = reader.ReadChars(length);
			return new string(chars);
		}
		
		public static T[] ReadArray<T>(this BinaryReader reader, Func<BinaryReader, T> deserializeFunc)
		{
			int length = reader.ReadInt32();
			return ReadArray(reader, deserializeFunc, length);
		}

		public static T[] ReadArray<T>(this BinaryReader reader, Func<BinaryReader, T> deserializeFunc, int length)
		{
			T[] array = new T[length];
			for (int i = 0; i < length; i++)
				array[i] = deserializeFunc(reader);

			return array;
		}

		public static List<T> ReadList<T>(this BinaryReader reader, Func<BinaryReader, T> deserializeFunc)
		{
			int length = reader.ReadInt32();
			return ReadList(reader, deserializeFunc, length);
		}

		public static List<T> ReadBytePrefixedList<T>(this BinaryReader reader, Func<BinaryReader, T> deserializeFunc)
		{
			byte length = reader.ReadByte();
			return ReadList(reader, deserializeFunc, length);
		}

		public static List<T> ReadList<T>(BinaryReader reader, Func<BinaryReader, T> deserializeFunc, int length)
		{
			List<T> list = new List<T>(length);
			for (int i = 0; i < length; i++)
				list.Add(deserializeFunc(reader));

			return list;
		}
	}
}
