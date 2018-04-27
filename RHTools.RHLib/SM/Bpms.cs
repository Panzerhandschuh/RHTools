using RHTools.Serialization.Serialization;
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
            Bpm bpm = new Bpm();

            string[] values = bpmValue.Split('=');
            bpm.beat = float.Parse(values[0]);
            bpm.bpm = float.Parse(values[1]);

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
            Bpms bpms = new Bpms();

            string[] bpmValues = parameters[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string bpmValue in bpmValues)
                bpms.bpms.Add(Bpm.Deserialize(bpmValue));

            return bpms;
        }
    }
}
