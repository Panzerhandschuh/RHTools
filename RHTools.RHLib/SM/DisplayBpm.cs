using RHTools.RHLib.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.SM
{
    public enum DisplayBpmType
    {
        Specified,
        Random
    }

    public class DisplayBpm : ITextSerializable
	{
        public DisplayBpmType bpmType;
        public float minBpm;
        public float maxBpm;

        public void Serialize(StreamWriter writer)
        {
            throw new NotImplementedException();
        }

        public static DisplayBpm Deserialize(List<string> parameters)
        {
            DisplayBpm displayBpm = new DisplayBpm();

            if (parameters[0] == "*")
                displayBpm.bpmType = DisplayBpmType.Random;
            else
            {
                displayBpm.bpmType = DisplayBpmType.Specified;
                displayBpm.minBpm = float.Parse(parameters[1]);
                if (parameters.Count > 2)
                    displayBpm.maxBpm = float.Parse(parameters[2]);
                else
                    displayBpm.maxBpm = displayBpm.minBpm;
            }

            return displayBpm;
        }
    }
}
