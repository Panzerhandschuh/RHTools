using RHTools.Serialization.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class Artist : IBinarySerializable
	{
		public string artist;
		public ArtistType type;

		public void Serialize(BinaryWriter writer)
		{
			writer.Write((byte)CacheEntryType.ArtistName);
			writer.WriteShortPrefixedString(artist);
			writer.Write((byte)CacheEntryType.ArtistType);
			writer.Write((byte)type);
			writer.Write((byte)CacheEntryType.EndOfEntry);
		}

		public static Artist Deserialize(BinaryReader reader)
		{
			Artist artist = new Artist();

			reader.ReadByte(); // ALways 0x21? Probably is an artist name indicator
			artist.artist = reader.ReadShortPrefixedString();
			reader.ReadByte(); // Always 0x23? Probably is an artist type indicator
			artist.type = (ArtistType)reader.ReadByte();
			reader.ReadByte(); // Always 0xFF? Probably indicates the end of the entry

			return artist;
		}
	}
}
