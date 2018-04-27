using RHTools.Serialization.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.SM
{
    public class BackgroundChange : ITextSerializable
    {
        public float startBeat;
        public string file1;
        public string file2;
        public float rate;
        public string transition;
        public string effect;
        public string color1;
        public string color2;

        public void Serialize(StreamWriter writer)
        {
            throw new NotImplementedException();
        }

        public static BackgroundChange Deserialize(string changeValue)
        {
            BackgroundChange change = new BackgroundChange();

            string[] values = changeValue.Split('=');
            for (int i = 0; i < values.Length; i++)
            {
                string value = values[i];
                switch (i)
                {
                    case 0:
                        change.startBeat = float.Parse(value);
                        break;
                    case 1:
                        change.file1 = value; // TODO: Add conversion
                        break;
                    case 2:
                        change.rate = float.Parse(value);
                        break;
                    case 3:
                        change.transition = value; // TODO: Add conversion
                        break;
                    case 4:
                        change.effect = value; // TODO: Add conversion
                        break;
                    case 5:
                        change.effect = value; // TODO: Add conversion
                        break;
                    case 6:
                        change.effect = value;
                        break;
                    case 7:
                        change.file2 = value; // TODO: Add conversion
                        break;
                    case 8:
                        change.transition = value;
                        break;
                    case 9:
                        change.color1 = value; // TODO: Add converstion
                        break;
                    case 10:
                        change.color2 = value; // TODO: Add converstion
                        break;
                }
            }

            return change;
        }
    }

    public class BackgroundChanges
    {
        public List<BackgroundChange> changes;

        public BackgroundChanges()
        {
            changes = new List<BackgroundChange>();
        }

        public void Serialize(StreamWriter writer)
        {
            throw new NotImplementedException();
        }

        public static BackgroundChanges Deserialize(List<string> parameters)
        {
            BackgroundChanges changes = new BackgroundChanges();

            string[] bgChangeValues = parameters[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string bgChangeValue in bgChangeValues)
                changes.changes.Add(BackgroundChange.Deserialize(bgChangeValue));

            return changes;
        }
    }
}
