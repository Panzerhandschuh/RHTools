using RHTools.Mixer.Utils;
using RHTools.Serialization;
using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Mixer
{
    public class Mixer
    {
        private RhcFile rhcFile;

        public Mixer(string rhcPath)
        {
            rhcFile = IBinarySerializableExtensions.Deserialize(rhcPath, RhcFile.Deserialize);
        }

        public void Mix()
        {
            Layer layer = rhcFile.layers.layers.First();
            List<BeatNotes> beatNotesList = NoteConverter.ConvertToBeatNotes(layer.notes);

            List<BeatNotes> mixedBeatNotesList = MixBeatNotes(beatNotesList);

            Dictionary<NoteFlags, List<Note>> rhNotes = NoteConverter.ConvertToRhNotes(mixedBeatNotesList);
            layer.notes = rhNotes;
        }

        public List<BeatNotes> MixBeatNotes(List<BeatNotes> beatNotesList)
        {
            List<BeatNotes> mixedBeatNotesList = new List<BeatNotes>();

            foreach (BeatNotes beatNotes in beatNotesList)
            {

            }

            return mixedBeatNotesList;
        }
    }
}
