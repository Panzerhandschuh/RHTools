﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class TabsFile : IBinarySerializable
	{
		byte version;
		public List<RhGuid> rhprojFileGuids;

		public TabsFile()
		{
			rhprojFileGuids = new List<RhGuid>();
		}

		public void Serialize(BinaryWriter writer)
		{
			writer.Write(version);
			writer.Write((byte)rhprojFileGuids.Count);
			foreach (RhGuid rhprojFileGuid in rhprojFileGuids)
				writer.Write(rhprojFileGuid);
		}

		public static TabsFile Deserialize(BinaryReader reader)
		{
			TabsFile file = new TabsFile();

			file.version = reader.ReadByte();
			byte numTabs = reader.ReadByte();
			for (int i = 0; i < numTabs; i++)
				file.rhprojFileGuids.Add(reader.ReadRhGuid());

			return file;
		}
	}
}
