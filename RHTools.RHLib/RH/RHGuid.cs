using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class RhGuid
	{
		public byte[] guid;

		public RhGuid(byte[] bytes)
		{
			guid = bytes;
		}

		public override string ToString()
		{
			return BitConverter.ToString(guid);
		}
	}
}
