using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class CacheFile : IBinarySerializable
	{
		public byte version; // Uncertain, but most likely a version number
		public OggCacheEntry[] oggEntries;
		public PngCacheEntry[] pngEntries;
		public RhsCacheEntry[] rhsEntries;
		public RhcCacheEntry[] rhcEntries;

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static CacheFile Deserialize(BinaryReader reader)
		{
			CacheFile file = new CacheFile();

			file.version = reader.ReadByte();
			file.oggEntries = reader.ReadArray(OggCacheEntry.Deserialize);
			file.pngEntries = reader.ReadArray(PngCacheEntry.Deserialize);
			file.rhsEntries = reader.ReadArray(RhsCacheEntry.Deserialize);
			file.rhcEntries = reader.ReadArray(RhcCacheEntry.Deserialize);

			return file;
		}
	}
}
