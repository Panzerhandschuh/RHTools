using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class RhGuid
	{
		public byte[] guid;

		public RhGuid(byte[] bytes)
		{
			guid = bytes;
		}

		public static bool operator ==(RhGuid guid1, RhGuid guid2)
		{
			return Enumerable.SequenceEqual(guid1.guid, guid2.guid);
		}

		public static bool operator !=(RhGuid guid1, RhGuid guid2)
		{
			return !(guid1 == guid2);
		}

		public override bool Equals(object obj)
		{
			RhGuid otherGuid = obj as RhGuid;
			if (otherGuid == null)
				return false;

			return (this == otherGuid);
		}

		public override string ToString()
		{
			return BitConverter.ToString(guid);
		}
	}
}
