using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Mixer.Utils
{
    public static class NoteConverter
    {
        /// <summary>
        /// Converts Rhythm Horizon's note structure (a dictionary of panels paired with a list of notes for each panel) into the BeatNotes structure (a list of beats paired with all notes for each beat)
        /// The resulting structure is ordered by beat
        /// </summary>
        public static List<BeatNotes> ConvertToBeatNotes(Dictionary<NoteFlags, List<Note>> rhNotes)
        {
            List<BeatNotes> beatNotesList = new List<BeatNotes>();

            Dictionary<int, List<Note>> beatDictionary = ConvertToBeatDictionary(rhNotes);
            foreach (KeyValuePair<int, List<Note>> kvs in beatDictionary)
                beatNotesList.Add(new BeatNotes(kvs.Key, kvs.Value));

            return beatNotesList.OrderBy(x => x.beat).ToList();
        }

        /// <summary>
        /// Groups Rhythm Horizon notes by beat
        /// The panel does not matter since it will be randomized later
        /// </summary>
        private static Dictionary<int, List<Note>> ConvertToBeatDictionary(Dictionary<NoteFlags, List<Note>> rhNotes)
        {
            Dictionary<int, List<Note>> beatDictionary = new Dictionary<int, List<Note>>();

            foreach (List<Note> notes in rhNotes.Values)
            {
                foreach (Note note in notes)
                {
                    List<Note> value;
                    if (beatDictionary.TryGetValue(note.beat, out value))
                        value.Add(note);
                    else
                        beatDictionary.Add(note.beat, new List<Note>());
                }
            }

            return beatDictionary;
        }

        public static Dictionary<NoteFlags, List<Note>> ConvertToRhNotes(List<BeatNotes> beatNotes)
        {
            throw new NotImplementedException();
        }
    }
}
