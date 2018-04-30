using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.Extensions
{
	public static class ITextSerializableExtensions
	{
		public static T Deserialize<T>(string path, Func<StreamReader, T> deserializeFunc) where T : ITextSerializable
		{
			using (Stream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (StreamReader reader = new StreamReader(stream))
			{
				return deserializeFunc(reader);
			}
		}
	}
}
