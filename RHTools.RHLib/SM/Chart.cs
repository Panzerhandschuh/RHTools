using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.SM
{
    public class Chart : ITextSerializable
	{
        public string chartType;
        public string description;
        public Difficulty difficulty;
        public int meter;
        public float[] grooveRadarValues; // Unused
        public NoteData noteData;

        public void Serialize(StreamWriter writer)
        {
            throw new NotImplementedException();
        }

        public static Chart Deserialize(List<string> parameters)
		{
			Chart notes = new Chart();

			notes.chartType = parameters[1];
			notes.description = parameters[2];
			notes.difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), parameters[3]);
			notes.meter = int.Parse(parameters[4]);
			notes.grooveRadarValues = GetGrooveRadarValues(parameters[5]);
			notes.noteData = NoteData.Deserialize(parameters[6]);

			return notes;
		}

		private static float[] GetGrooveRadarValues(string parameter)
		{
			return parameter.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(x => float.Parse(x)).ToArray();
		}
	}
}
