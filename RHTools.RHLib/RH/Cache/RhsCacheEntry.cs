using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class RhsCacheEntry : IBinarySerializable
	{
		public RhGuid rhsGuid;
		public RhGuid internalGuid;
		public RhGuid oggGuid;
		public RhGuid pngGuid;
		public string chartName;
		public TimingData timingData;
		public List<Artist> artists;
		public float displayBpm; // Always -1. Uncertain if this is the actual meaning (maybe values other than -1 override the song BPM).
		public float previewStart;
		public float previewLength;
		public string displayArtist;

		public RhsCacheEntry()
		{
			artists = new List<Artist>();
		}

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static RhsCacheEntry Deserialize(BinaryReader reader)
		{
			RhsCacheEntry entry = new RhsCacheEntry();

			// TODO: Remove duplicate logic for parsing cache entry types
			CacheEntryType type;
			while ((type = (CacheEntryType)reader.ReadByte()) != CacheEntryType.EndOfEntry)
			{
				switch (type)
				{
					case CacheEntryType.Internal:
						entry.internalGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Rhs:
						entry.rhsGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Ogg:
						entry.oggGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Png:
						entry.pngGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.ChartName:
						entry.chartName = reader.ReadShortPrefixedString();
						break;
					case CacheEntryType.TimingData:
						entry.timingData = TimingData.Deserialize(reader);
						break;
					case CacheEntryType.DisplayBpm:
						entry.displayBpm = reader.ReadSingle(); // Always -1
						break;
					case CacheEntryType.PreviewStart:
						entry.previewStart = reader.ReadSingle();
						break;
					case CacheEntryType.PreviewLength:
						entry.previewLength = reader.ReadSingle();
						break;
					case CacheEntryType.DisplayArtist:
						entry.displayArtist = reader.ReadShortPrefixedString();
						break;
					case CacheEntryType.Artists:
						entry.artists.Add(Artist.Deserialize(reader));
						break;
					default:
						throw new Exception("Unknown cache entry type: " + type);
				}
			}
			
			return entry;
		}
	}
}
