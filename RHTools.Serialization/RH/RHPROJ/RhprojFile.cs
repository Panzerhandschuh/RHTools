using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class RhprojFile : IBinarySerializable
	{
		public byte version;
		public byte unknown1;

		public RhcFile rhcFile;
		public byte[] unknown2;
		public RhsFile rhsFile;
		public byte[] unknown3;

		public void Serialize(BinaryWriter writer)
		{
			writer.Write(version);
			writer.Write(unknown1);

			rhcFile.Serialize(writer);
			writer.Write(unknown2);
			rhsFile.Serialize(writer);
			writer.Write(unknown3);
		}

		public static RhprojFile Deserialize(BinaryReader reader)
		{
			RhprojFile file = new RhprojFile();

			file.version = reader.ReadByte();
			file.unknown1 = reader.ReadByte(); // Always 0?

			file.rhcFile = RhcFile.Deserialize(reader);
			file.unknown2 = reader.ReadBytes(8);
			file.rhsFile = RhsFile.Deserialize(reader);
			file.unknown3 = reader.ReadBytes(16);

			return file;
		}
	}
}
