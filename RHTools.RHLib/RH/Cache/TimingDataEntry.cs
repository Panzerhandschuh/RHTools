using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class TimingDataEntry : IBinarySerializable
	{
		public byte[] unknown1;

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static TimingDataEntry Deserialize(BinaryReader reader)
		{
			TimingDataEntry entry = new TimingDataEntry();

			entry.unknown1 = reader.ReadBytes(12);

			return entry;
		}
	}
}
