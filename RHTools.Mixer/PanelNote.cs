using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Mixer
{
    public class PanelNote
    {
        public NoteFlags panel;
        public Note note;

        public PanelNote(NoteFlags panel, Note note)
        {
            this.panel = panel;
            this.note = note;
        }
    }
}
