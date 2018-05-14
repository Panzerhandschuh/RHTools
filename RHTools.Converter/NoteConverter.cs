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
		private class BeatNotes
		{
			public int beat;
			public List<SmNoteType> notes;

			public BeatNotes(int beat, List<SmNoteType> notes)
			{
				this.beat = beat;
				this.notes = notes;
			}
		}

		public Dictionary<NoteFlags, List<Note>> Convert(Chart chart)
		{
			Dictionary<NoteFlags, List<Note>> noteDict = new Dictionary<NoteFlags, List<Note>>();

			int numPanels = chart.GetPanelCount();
			List<BeatNotes> beatNotesList = GetBeatNotes(chart.noteData.measures);
			for (int panel = 0; panel < numPanels; panel++)
			{
				NoteFlags rhNote = GetRhNote(panel);
				List<Note> notes = GetNotesForPanel(beatNotesList, panel);
				noteDict.Add(rhNote, notes);
			}

			return noteDict;
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

		private List<Note> GetNotesForPanel(List<BeatNotes> beatNotesList, int panel)
		{
			List<Note> notes = new List<Note>();
			int holdStartBeat = 0;

			foreach (BeatNotes beatNotes in beatNotesList)
			{
				int beat = beatNotes.beat;
				SmNoteType note = beatNotes.notes[panel];
				switch (note)
				{
					case SmNoteType.Regular:
						notes.Add(new Note(RhNoteType.Regular, beat));
						break;
					case SmNoteType.Hold:
					case SmNoteType.Roll:
						holdStartBeat = beatNotes.beat;
						break;
					case SmNoteType.Tail:
						notes.Add(new Note(RhNoteType.Hold, holdStartBeat, beat - holdStartBeat));
						break;
					case SmNoteType.Mine:
						notes.Add(new Note(RhNoteType.Mine, beat));
						break;
				}
			}

			return notes;
		}

		/// <summary>
		/// Convert measures into an intermediate data structure so that it is easier to process
		/// This structure pairs each line of notes with a beat
		/// </summary>
		private List<BeatNotes> GetBeatNotes(List<Measure> measures)
		{
			List<BeatNotes> beatNotesList = new List<BeatNotes>();

			for (int i = 0; i < measures.Count; i++)
			{
				Measure measure = measures[i];
				int measureBeat = i * 4000;

				List<Line> lines = measure.lines;
				for (int j = 0; j < lines.Count; j++)
				{
					Line line = lines[j];
					int lineBeat = (j * 4000) / lines.Count;
					int beat = measureBeat + lineBeat;

					List<SmNoteType> notes = line.notes;
					beatNotesList.Add(new BeatNotes(beat, notes));
				}
			}

			return beatNotesList;
		}
	}
}
