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
        public string difficulty;
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
			notes.difficulty = parameters[3];
			notes.meter = int.Parse(parameters[4]);
			notes.grooveRadarValues = GetGrooveRadarValues(parameters[5]);
			notes.noteData = NoteData.Deserialize(parameters[6]);

			return notes;
		}

		private static float[] GetGrooveRadarValues(string parameter)
		{
			string[] strValues = parameter.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			float[] values = new float[strValues.Length];
			for (int i = 0; i < strValues.Length; i++)
				float.TryParse(strValues[i], out values[i]);

			return values;
		}
	}
}
