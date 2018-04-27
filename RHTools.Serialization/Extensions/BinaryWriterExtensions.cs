using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization
{
	public static class BinaryWriterExtensions
	{
		public static void Write(this BinaryWriter writer, RhGuid guid)
		{
			writer.Write(guid.guid);
		}

		public static void WriteShortPrefixedString(this BinaryWriter writer, string str)
		{
			writer.Write((ushort)str.Length);
			writer.Write(str.ToCharArray());
		}

		public static void Write(this BinaryWriter writer, IBinarySerializable obj)
		{
			obj.Serialize(writer);
		}

		public static void Write(this BinaryWriter writer, IEnumerable<IBinarySerializable> objs, bool isLengthPrefixed = true)
		{
			if (isLengthPrefixed)
				writer.Write(objs.Count());

			foreach (IBinarySerializable obj in objs)
				obj.Serialize(writer);
		}

		public static void WriteBytePrefixedList(this BinaryWriter writer, IEnumerable<IBinarySerializable> objs)
		{
			writer.Write((byte)objs.Count());

			foreach (IBinarySerializable obj in objs)
				obj.Serialize(writer);
		}

		/// <summary>
		/// Writes data with a byte prefix if the data is not null
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="writer"></param>
		/// <param name="prefix"></param>
		/// <param name="obj"></param>
		/// <param name="writeAction">This is needed for the BinaryWriter to resolve types</param>
		public static void WriteOptionalData<T>(this BinaryWriter writer, byte prefix, T obj, Action<T> writeAction)
		{
			if (obj != null)
			{
				writer.Write(prefix);
				writeAction(obj);
			}
		}

		/// <summary>
		/// Writes a list of objects with a prefix in front of each object
		/// </summary>
		public static void WritePrefixedList<T>(this BinaryWriter writer, byte prefix, IEnumerable<T> objs, Action<T> writeAction)
		{
			foreach (T obj in objs)
			{
				writer.Write(prefix);
				writeAction(obj);
			}
		}
	}
}
