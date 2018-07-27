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
            var beatNotesList = new List<BeatNotes>();

			var beatDictionary = ConvertToBeatDictionary(rhNotes);
            foreach (var kvs in beatDictionary)
                beatNotesList.Add(new BeatNotes(kvs.Key, kvs.Value));

            return beatNotesList.OrderBy(x => x.beat).ToList();
        }

        /// <summary>
        /// Groups Rhythm Horizon notes by beat
        /// The panel does not matter since it will be randomized later
        /// </summary>
        private static Dictionary<int, List<Note>> ConvertToBeatDictionary(Dictionary<NoteFlags, List<Note>> rhNotes)
        {
            var beatDictionary = new Dictionary<int, List<Note>>();

            foreach (var notes in rhNotes.Values)
            {
                foreach (var note in notes)
                {
					var beat = note.beat;
					if (!beatDictionary.ContainsKey(beat))
						beatDictionary.Add(beat, new List<Note>());

					beatDictionary[beat].Add(note);
                }
            }

            return beatDictionary;
        }

        public static Dictionary<NoteFlags, List<Note>> ConvertToRhNotes(List<List<PanelNote>> panelNotesList)
        {
			var rhNotes = new Dictionary<NoteFlags, List<Note>>();

            foreach (var panelNotes in panelNotesList)
			{
				foreach (var panelNote in panelNotes)
				{
					if (!rhNotes.ContainsKey(panelNote.panel))
						rhNotes.Add(panelNote.panel, new List<Note>());

					rhNotes[panelNote.panel].Add(panelNote.note);
				}
			}

			return rhNotes;
        }
    }
}
