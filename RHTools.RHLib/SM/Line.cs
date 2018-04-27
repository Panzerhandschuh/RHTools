using RHTools.Serialization.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.SM
{
    public enum NoteType
    {
        None,
        Regular,
        Hold,
        Tail,
        Roll,
        Mine
    }

    public class Line : ITextSerializable
	{
        List<NoteType> notes;

        public Line()
        {
            notes = new List<NoteType>();
        }

        public void Serialize(StreamWriter writer)
        {
            throw new NotImplementedException();
        }

        public static Line Deserialize(string lineValue)
        {
            Line line = new Line();

            foreach (char noteValue in lineValue)
                line.notes.Add(GetNoteType(noteValue));

            return line;
        }

        public static NoteType GetNoteType(char noteValue)
        {
            switch (noteValue)
            {
                case '0':
                    return NoteType.None;
                case '1':
                    return NoteType.Regular;
                case '2':
                    return NoteType.Hold;
                case '3':
                    return NoteType.Tail;
                case '4':
                    return NoteType.Roll;
                case 'M':
                    return NoteType.Mine;
                default:
                    return NoteType.None;
            }
        }
    }
}
