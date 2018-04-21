﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class Artist : IBinarySerializable
	{
		public string artist;
		public ArtistType type;

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static Artist Deserialize(BinaryReader reader)
		{
			Artist artist = new Artist();

			reader.ReadByte(); // ALways 0x21? This might be an artist name indicator
			artist.artist = reader.ReadShortPrefixedString();
			reader.ReadByte(); // Always 0x23? This might be an artist type indicator
			artist.type = (ArtistType)reader.ReadByte();
			reader.ReadByte(); // Always 0xFF? Probably indicates the end of the entry

			return artist;
		}
	}
}