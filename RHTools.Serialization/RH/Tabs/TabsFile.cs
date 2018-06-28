using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class TabsFile : IBinarySerializable
	{
		private byte version;
		public List<RhGuid> rhprojFileGuids;

		public TabsFile()
		{
			rhprojFileGuids = new List<RhGuid>();
		}

		public void Serialize(BinaryWriter writer)
		{
			writer.Write(version);
			writer.Write((byte)rhprojFileGuids.Count);
			foreach (var rhprojFileGuid in rhprojFileGuids)
				writer.Write(rhprojFileGuid);
		}

		public static TabsFile Deserialize(BinaryReader reader)
		{
			var file = new TabsFile();

			file.version = reader.ReadByte();
			var numTabs = reader.ReadByte();
			for (var i = 0; i < numTabs; i++)
				file.rhprojFileGuids.Add(reader.ReadRhGuid());

			return file;
		}
	}
}
