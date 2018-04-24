﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class RhcFile : IBinarySerializable
	{
		public byte version;
		public RhGuid rhcGuid;
		public RhGuid internalGuid;
		public RhGuid rhsGuid;
		public string chartName;
		public byte[] unknown1;
		public List<Artist> artists;
		public Layers layers;

		public RhcFile()
		{
			artists = new List<Artist>();
		}

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static RhcFile Deserialize(BinaryReader reader)
		{
			RhcFile file = new RhcFile();

			file.version = reader.ReadByte();

			RhcEntryType type;
			while ((type = (RhcEntryType)reader.ReadByte()) != RhcEntryType.EndOfEntry)
			{
				switch (type)
				{
					case RhcEntryType.Rhc:
						file.rhcGuid = reader.ReadRhGuid();
						break;
					case RhcEntryType.Rhs:
						file.rhsGuid = reader.ReadRhGuid();
						break;
					case RhcEntryType.Layers:
						file.layers = Layers.Deserialize(reader);
						break;
					case RhcEntryType.ChartName:
						file.chartName = reader.ReadShortPrefixedString();
						break;
					case RhcEntryType.Unknown1:
						file.unknown1 = reader.ReadBytes(2);
						break;
					case RhcEntryType.Internal:
						file.internalGuid = reader.ReadRhGuid();
						break;
					case RhcEntryType.Artists:
						file.artists.Add(Artist.Deserialize(reader));
						break;
					default:
						throw new Exception("Unknown chart entry type: " + type);
				}
			}

			return file;
		}
	}
}
