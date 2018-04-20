using RHTools.RHLib.RH;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib
{
	public static class BinaryReaderExtensions
	{
		public static RhGuid ReadRHGuid(this BinaryReader reader)
		{
			byte[] bytes = reader.ReadBytes(16);
			return new RhGuid(bytes);
		}
		
		public static T[] ReadArray<T>(this BinaryReader reader, Func<T> deserializeFunc)
		{
			int numItems = reader.ReadInt32();
			T[] array = new T[numItems];
			for (int i = 0; i < numItems; i++)
				array[i] = deserializeFunc();

			return array;
		}
	}
}
