﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.Serialization
{
	public static class StreamExtensions
	{
		public static byte[] ReadAllBytes(this Stream stream)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
		}
	}
}
