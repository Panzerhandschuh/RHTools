using RHTools.RHLib.RH;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.Serialization
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
	}
}
