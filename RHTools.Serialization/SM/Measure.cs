using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.SM
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
            var measure = new Measure();

            var lineValues = measureValue.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var lineValue in lineValues)
                measure.lines.Add(Line.Deserialize(lineValue));

            return measure;
        }
    }
}
