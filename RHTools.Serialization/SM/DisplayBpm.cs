using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.SM
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
            var displayBpm = new DisplayBpm();

            if (parameters[0] == "*")
                displayBpm.bpmType = DisplayBpmType.Random;
            else
            {
                displayBpm.bpmType = DisplayBpmType.Specified;
				float.TryParse(parameters[1], out displayBpm.minBpm);
				if (parameters.Count > 2)
					float.TryParse(parameters[2], out displayBpm.maxBpm);
                else
                    displayBpm.maxBpm = displayBpm.minBpm;
            }

            return displayBpm;
        }
    }
}
