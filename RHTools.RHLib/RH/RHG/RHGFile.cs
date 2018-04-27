using RHTools.RHLib.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class RhgFile : IBinarySerializable
	{
		public byte version;
		public RhGuid rhgGuid;
		public RhGuid internalGuid;
		public RhGuid pngGuid;
		public string packName;
		public List<RhGuid> rhcGuids;

		public RhgFile()
		{
			rhcGuids = new List<RhGuid>();
		}

		public void Serialize(BinaryWriter writer)
		{
			writer.Write(version);

			writer.WriteOptionalData((byte)RhgEntryType.Rhg, rhgGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)RhgEntryType.Internal, internalGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)RhgEntryType.Png, pngGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)RhgEntryType.PackName, packName, (x) => writer.WriteShortPrefixedString(x));
			writer.WritePrefixedList((byte)RhgEntryType.Rhc, rhcGuids, (x) => writer.Write(x));
			writer.Write((byte)RhgEntryType.EndOfEntry);
		}

		public static RhgFile Deserialize(BinaryReader reader)
		{
			RhgFile file = new RhgFile();

			file.version = reader.ReadByte();

			RhgEntryType type;
			while ((type = (RhgEntryType)reader.ReadByte()) != RhgEntryType.EndOfEntry)
			{
				switch (type)
				{
					case RhgEntryType.Rhg:
						file.rhgGuid = reader.ReadRhGuid();
						break;
					case RhgEntryType.Internal:
						file.internalGuid = reader.ReadRhGuid();
						break;
					case RhgEntryType.Png:
						file.pngGuid = reader.ReadRhGuid();
						break;
					case RhgEntryType.PackName:
						file.packName = reader.ReadShortPrefixedString();
						break;
					case RhgEntryType.Rhc:
						file.rhcGuids.Add(reader.ReadRhGuid());
						break;
					default:
						throw new Exception("Unknown group entry type: " + type);
				}
			}

			return file;
		}
	}
}
