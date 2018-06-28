using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.SM
{
    public class Bpm : ITextSerializable
	{
        public float beat;
        public float bpm;

        public void Serialize(StreamWriter writer)
        {
            throw new NotImplementedException();
        }

        public static Bpm Deserialize(string bpmValue)
        {
            var bpm = new Bpm();

            var values = bpmValue.Split('=').Select(x => x.Trim()).ToArray();
			float.TryParse(values[0], out bpm.beat);
			float.TryParse(values[1], out bpm.bpm);

			return bpm;
        }
    }

    public class Bpms : ITextSerializable
	{
        public List<Bpm> bpms;

        public Bpms()
        {
            bpms = new List<Bpm>();
        }

        public void Serialize(StreamWriter writer)
        {
            throw new NotImplementedException();
        }

        public static Bpms Deserialize(List<string> parameters)
        {
            var bpms = new Bpms();

            var bpmValues = parameters[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var bpmValue in bpmValues)
                bpms.bpms.Add(Bpm.Deserialize(bpmValue));

            return bpms;
        }
    }
}
