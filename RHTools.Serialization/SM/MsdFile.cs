using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.SM
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
			var file = new MsdFile();

			var text = reader.ReadToEnd();
			var tags = ReadTags(text);
			file.values = ReadMsdValues(tags);

			return file;
		}

		private static List<string> ReadTags(string text)
		{
			var tags = new List<string>();

			var formattedText = FormatText(text);
			var data = formattedText.ToCharArray();

			for (var i = 0; i < data.Length; i++)
			{
				if (IsStartOfTag(data, i))
					i = ReadTag(tags, data, i);
			}

			return tags;
		}

		/// <summary>
		/// Removes comments, whitespace, and empty lines from the specified text
		/// </summary>
		private static string FormatText(string text)
		{
			var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			var validLines = new List<string>();
			foreach (var line in lines)
			{
				var formattedLine = FormatLine(line);
				if (!string.IsNullOrEmpty(formattedLine))
					validLines.Add(formattedLine);
			}

			return string.Join(Environment.NewLine, validLines);
		}

		private static string FormatLine(string line)
		{
			var commentIndex = line.IndexOf("//");
			var formattedLine = (commentIndex != -1) ? line.Substring(0, commentIndex) : line;
			return formattedLine.Trim();
		}

		private static int ReadTag(List<string> tags, char[] data, int i)
		{
			i++;
			var start = i;

			i = FindEndOfTag(data, i);
			var end = i;

			var tag = new string(data, start, end - start);
			tags.Add(tag);

			return i;
		}

		private static int FindEndOfTag(char[] data, int i)
		{
			while (i < data.Length)
			{
				i++;

				if (IsEndOfTag(data, i))
					return i;

				if (IsStartOfTag(data, i))
					return i - 1;
			}

			return i;
		}

		private static bool IsStartOfTag(char[] data, int i)
		{
			return data[i] == '#';
		}

		private static bool IsEndOfTag(char[] data, int i)
		{
			return data[i] == ';';
		}

		private static List<MsdValue> ReadMsdValues(List<string> tags)
		{
			var values = new List<MsdValue>();

			foreach (var tag in tags)
			{
				var parameters = tag.Split(':').Select(x => x.Trim()).ToList();
				values.Add(new MsdValue(parameters));
			}

			return values;
		}
	}
}
