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

		public RhGuid()
		{
			guid = new byte[16];
		}

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
			var otherGuid = obj as RhGuid;
			if (otherGuid == null)
				return false;

			return (this == otherGuid);
		}

		public override int GetHashCode()
		{
			return -1324198676 + EqualityComparer<byte[]>.Default.GetHashCode(guid);
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			for (var i = 0; i < guid.Length; i++)
			{
				if (i == 4 || i == 6 || i == 8 || i == 10)
					sb.Append('-');

				sb.Append(guid[i].ToString("x2"));
			}

			return sb.ToString();
		}

		public static RhGuid NewGuid()
		{
			var guid = Guid.NewGuid();
			return new RhGuid(guid.ToByteArray());
		}
	}
}
