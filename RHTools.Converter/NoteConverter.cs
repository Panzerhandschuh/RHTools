using RHTools.Serialization.RH;
using RHTools.Serialization.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RhNoteType = RHTools.Serialization.RH.NoteType;
using SmNoteType = RHTools.Serialization.SM.NoteType;

namespace RHTools.Converter
{
	public class NoteConverter
	{
		public Dictionary<NoteFlags, List<Note>> Convert(Chart chart)
		{
			Dictionary<NoteFlags, List<Note>> noteDict = new Dictionary<NoteFlags, List<Note>>();

			int numPanels = chart.GetPanelCount();
			for (int panel = 0; panel < numPanels; panel++)
			{
				NoteFlags rhNote = GetRhNote(panel);
				List<Note> notes = GetNotesForPanel(chart.noteData, panel);
				noteDict.Add(rhNote, notes);
			}

			return noteDict;
		}

		private List<Note> GetNotesForPanel(NoteData noteData, int panel)
		{
			List<Note> notes = new List<Note>();
			int beat = 0;
			int holdStartBeat = 0;

			// TODO: Simplify logic by converting NoteData into a temporary data structure (pair beat # with notes)
			foreach (Measure measure in noteData.measures)
			{
				int beatIncrement = 4000 / measure.lines.Count;
				foreach (Line line in measure.lines)
				{
					SmNoteType note = line.notes[panel];
					switch (note)
					{
						case SmNoteType.Regular:
							notes.Add(new Note(RhNoteType.Regular, beat));
							break;
						case SmNoteType.Hold:
						case SmNoteType.Roll:
							holdStartBeat = beat;
							break;
						case SmNoteType.Tail:
							notes.Add(new Note(RhNoteType.Hold, beat, beat - holdStartBeat));
							break;
						case SmNoteType.Mine:
							notes.Add(new Note(RhNoteType.Mine, beat));
							break;
					}

					beat += beatIncrement;
				}
			}

			return notes;
		}

		private NoteFlags GetRhNote(int smPanel)
		{
			switch (smPanel)
			{
				case 0:
					return NoteFlags.Pad1Left;
				case 1:
					return NoteFlags.Pad1Down;
				case 2:
					return NoteFlags.Pad1Up;
				case 3:
					return NoteFlags.Pad1Right;
				case 4:
					return NoteFlags.Pad2Left;
				case 5:
					return NoteFlags.Pad2Down;
				case 6:
					return NoteFlags.Pad2Up;
				case 7:
					return NoteFlags.Pad2Right;
				default:
					throw new ArgumentException();
			}
		}
	}
}
