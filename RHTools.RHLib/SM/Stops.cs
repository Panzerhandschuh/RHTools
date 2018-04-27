using RHTools.Serialization.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.SM
{
    public class Stop : ITextSerializable
	{
        public float beat;
        public float seconds;

        public void Serialize(StreamWriter writer)
        {
            throw new NotImplementedException();
        }

        public static Stop Deserialize(string stopValue)
        {
            Stop stop = new Stop();

            string[] values = stopValue.Split('=');
            stop.beat = float.Parse(values[0]);
            stop.seconds = float.Parse(values[1]);

            return stop;
        }
    }

    public class Stops : ITextSerializable
	{
        public List<Stop> stops;

        public void Serialize(StreamWriter writer)
        {
            throw new NotImplementedException();
        }

        public static Stops Deserialize(List<string> parameters)
        {
            Stops stops = new Stops();

            string[] stopValues = parameters[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string stopValue in stopValues)
                stops.stops.Add(Stop.Deserialize(stopValue));

            return stops;
        }
    }
}
