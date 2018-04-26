using RHTools.RHLib.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.SM
{
    public class Measure : ITextSerializable
	{
        public List<Line> lines;

        public Measure()
        {
            lines = new List<Line>();
        }

        public void Serialize(StreamWriter writer)
        {
            throw new NotImplementedException();
        }

        public static Measure Deserialize(string measureValue)
        {
            Measure measure = new Measure();

            string[] lineValues = measureValue.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string lineValue in lineValues)
                measure.lines.Add(Line.Deserialize(lineValue));

            return measure;
        }
    }
}
