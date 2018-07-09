using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Mixer
{
    public class BeatNotes
    {
        public int beat;
        public List<Note> notes;

        public BeatNotes(int beat, List<Note> notes)
        {
            this.beat = beat;
            this.notes = notes;
        }
    }
}
