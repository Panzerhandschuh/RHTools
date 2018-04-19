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
		public static RHGuid ReadRHGuid(this BinaryReader reader)
		{
			byte[] bytes = reader.ReadBytes(16);
			return new RHGuid(bytes);
		}
	}
}
