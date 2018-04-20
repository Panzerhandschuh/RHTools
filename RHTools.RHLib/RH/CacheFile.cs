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
		public byte unknown1; // Version number?
		public OggCacheEntry[] oggEntries;
		public PngCacheEntry[] pngEntries;

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static CacheFile Deserialize(BinaryReader reader)
		{
			CacheFile file = new CacheFile();

			file.unknown1 = reader.ReadByte();
			file.oggEntries = reader.ReadArray(() => OggCacheEntry.Deserialize(reader));
			file.pngEntries = reader.ReadArray(() => PngCacheEntry.Deserialize(reader));

			return file;
		}
	}
}
