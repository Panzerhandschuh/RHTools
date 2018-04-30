using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public class RhsCacheEntry : IBinarySerializable
	{
		public RhGuid rhsGuid;
		public RhGuid internalGuid;
		public RhGuid oggGuid;
		public RhGuid pngGuid;
		public string songTitle;
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
			writer.WriteOptionalData((byte)CacheEntryType.Rhs, rhsGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.Internal, internalGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.Ogg, oggGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.Png, pngGuid, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.ChartName, songTitle, (x) => writer.WriteShortPrefixedString(x));
			writer.WriteOptionalData((byte)CacheEntryType.TimingData, timingData, (x) => writer.Write(x));
			writer.WritePrefixedList((byte)CacheEntryType.Artists, artists, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.DisplayBpm, displayBpm, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.PreviewStart, previewStart, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.PreviewLength, previewLength, (x) => writer.Write(x));
			writer.WriteOptionalData((byte)CacheEntryType.DisplayArtist, displayArtist, (x) => writer.WriteShortPrefixedString(x));
			writer.Write((byte)CacheEntryType.EndOfEntry);
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
					case CacheEntryType.Rhs:
						entry.rhsGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Internal:
						entry.internalGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Ogg:
						entry.oggGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.Png:
						entry.pngGuid = reader.ReadRhGuid();
						break;
					case CacheEntryType.ChartName:
						entry.songTitle = reader.ReadShortPrefixedString();
						break;
					case CacheEntryType.TimingData:
						entry.timingData = TimingData.Deserialize(reader);
						break;
					case CacheEntryType.Artists:
						entry.artists.Add(Artist.Deserialize(reader));
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
					default:
						throw new Exception("Unknown cache entry type: " + type);
				}
			}
			
			return entry;
		}
	}
}
