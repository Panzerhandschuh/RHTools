using RHTools.RHLib.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.SM
{
	/// <summary>
	/// File Format Sample:
	/// #PARAM0:PARAM1:PARAM2:PARAM3;
	/// #NEXTPARAM0:PARAM1:PARAM2:PARAM3;
	/// </summary>
	public class MsdFile : ITextSerializable
	{
		public List<MsdValue> values;

		public void Serialize(StreamWriter writer)
		{
			throw new NotImplementedException();
		}

		public static MsdFile Deserialize(StreamReader reader)
		{
			MsdFile file = new MsdFile();

			string text = reader.ReadToEnd();
			List<string> tags = ReadTags(text);
			file.values = ReadMsdValues(tags);

			return file;
		}

		private static List<string> ReadTags(string text)
		{
			List<string> tags = new List<string>();

			char[] data = text.ToCharArray();
			for (int i = 0; i < data.Length; i++)
			{
				if (IsStartOfComment(data, i))
					i = FindEndOfComment(data, i);

				if (IsStartOfTag(data, i))
					i = ReadTag(tags, data, i);
			}

			return tags;
		}

		private static bool IsStartOfComment(char[] data, int i)
		{
			return i + 1 < data.Length && data[i] == '/' && data[i + 1] == '/';
		}

		private static int FindEndOfComment(char[] data, int i)
		{
			return FindChar(data, i, '\n');
		}

		private static bool IsStartOfTag(char[] data, int i)
		{
			return data[i] == '#';
		}

		private static int ReadTag(List<string> tags, char[] data, int i)
		{
			i++;
			int start = i;

			i = FindEndOfTag(data, i);
			int end = i;

			string tag = new string(data, start, end - start);
			tags.Add(tag);

			return i;
		}

		private static int FindEndOfTag(char[] data, int i)
		{
			return FindChar(data, i, ';');
		}

		private static int FindChar(char[] data, int i, char c)
		{
			do
			{
				i++;
			} while (i < data.Length && data[i] != c);
			return i;
		}

		private static List<MsdValue> ReadMsdValues(List<string> tags)
		{
			List<MsdValue> values = new List<MsdValue>();

			foreach (string tag in tags)
			{
				List<string> parameters = tag.Split(':').Select(x => x.Trim()).ToList();
				values.Add(new MsdValue(parameters));
			}

			return values;
		}
	}
}
