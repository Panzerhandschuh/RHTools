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
		public byte unknown1; // Always 0?
		public int numOggEntries;
		public List<CacheEntry> entries;

		public CacheFile()
		{
			entries = new List<CacheEntry>();
		}

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static CacheFile Deserialize(BinaryReader reader)
		{
			CacheFile file = new CacheFile();

			file.unknown1 = reader.ReadByte();
			file.numOggEntries = reader.ReadInt32();
			for (int i = 0; i < file.numOggEntries; i++)
			{
				CacheEntry entry = CacheEntry.Deserialize(reader);
				file.entries.Add(entry);
			}

			return file;
		}
	}
}
