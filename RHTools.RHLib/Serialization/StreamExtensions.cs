using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.Serialization
{
	public static class StreamExtensions
	{
		public static byte[] GetBytes(this Stream stream)
		{
			using (MemoryStream memStream = new MemoryStream())
			{
				stream.CopyTo(memStream);
				return memStream.ToArray();
			}
		}
	}
}
