using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class CacheFile : IBinarySerializable
	{
		public byte version; // Uncertain, but most likely a version number
		public List<OggCacheEntry> oggEntries;
		public List<PngCacheEntry> pngEntries;
		public List<RhsCacheEntry> rhsEntries;
		public List<RhcCacheEntry> rhcEntries;
		public List<RhgCacheEntry> rhgEntries;

		public void Serialize(BinaryWriter writer)
		{
			writer.Write(version);

			writer.Write(oggEntries);
			writer.Write(pngEntries);
			writer.Write(rhsEntries);
			writer.Write(rhcEntries);
			writer.Write(rhgEntries);
		}

		public static CacheFile Deserialize(BinaryReader reader)
		{
			var file = new CacheFile();

			file.version = reader.ReadByte();
			file.oggEntries = reader.ReadList(OggCacheEntry.Deserialize);
			file.pngEntries = reader.ReadList(PngCacheEntry.Deserialize);
			file.rhsEntries = reader.ReadList(RhsCacheEntry.Deserialize);
			file.rhcEntries = reader.ReadList(RhcCacheEntry.Deserialize);
			file.rhgEntries = reader.ReadList(RhgCacheEntry.Deserialize);

			return file;
		}
	}
}
