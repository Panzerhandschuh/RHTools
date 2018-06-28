using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class OnlineFile : IBinarySerializable
	{
		public byte version;
		public List<RhGuid> fileGuids;

		public OnlineFile()
		{
			fileGuids = new List<RhGuid>();
		}

		public void Serialize(BinaryWriter writer)
		{
			writer.Write(version);
			foreach (var fileGuid in fileGuids)
				writer.Write(fileGuid);
		}

		public static OnlineFile Deserialize(BinaryReader reader)
		{
			var file = new OnlineFile();

			file.version = reader.ReadByte();
			while (reader.BaseStream.Position != reader.BaseStream.Length)
				file.fileGuids.Add(reader.ReadRhGuid());

			return file;
		}
	}
}
