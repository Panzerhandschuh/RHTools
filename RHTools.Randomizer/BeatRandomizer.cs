using RHTools.Randomizer.Generators;
using RHTools.Randomizer.Rules;
using RHTools.Randomizer.Utils;
using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer
{
	public class BeatRandomizer
	{
		private StepGenerator stepGenerator;
		private MineGenerator mineGenerator;
		private GeneratorState generatorState;

		private readonly BeatRandomizerSettings settings;

		public BeatRandomizer(BeatRandomizerSettings settings)
		{
			stepGenerator = new StepGenerator();
			mineGenerator = new MineGenerator();
			generatorState = new GeneratorState(settings.random);

			this.settings = settings;
		}

		/// <summary>
		/// Generates notes for a single beat
		/// </summary>
		/// <param name="originalNotes">Used by the randomizer to distinguish between regular notes, holds, and mines</param>
		/// <returns>Randomized notes for a beat</returns>
		public List<PanelNote> RandomizeBeat(List<Note> originalNotes)
		{
			var panelNotes = new List<PanelNote>();

			var sortedNotes = originalNotes.OrderBy(x => x.type);
			var maxNotesPerBeat = (settings.disableJumps) ? 1 : 2;
			var counter = new NoteCounter(settings.panelConfig, maxNotesPerBeat);
			var generatorInput = new GeneratorInput(generatorState, settings.panelConfig, settings.random);
			var beat = sortedNotes.First().beat;

			foreach (Note note in sortedNotes)
			{
				PanelNote panelNote;
				switch (note.type)
				{
					case NoteType.Regular:
					case NoteType.Hold:
						if (TryGenerateNote(generatorInput, beat, counter, out panelNote))
							panelNotes.Add(panelNote);
						break;
					case NoteType.Mine:
						if (TryGenerateMine(generatorInput, beat, out panelNote))
							panelNotes.Add(panelNote);
						break;
					default:
						throw new InvalidOperationException();
				}
			}

			return panelNotes;
		}

		private bool TryGenerateNote(GeneratorInput generatorInput, int beat, NoteCounter counter, out PanelNote panelNote)
		{
			if (counter.IsAtMaxNotes())
			{
				panelNote = null;
				return false;
			}

			if (!stepGenerator.TryGeneratePanel(generatorInput, settings.noteRules, out var generatedPanelIndices))
			{
				panelNote = null;
				return false;
			}

			RemovePanelFromAvailablePanels(generatorInput.availablePanels, generatedPanelIndices);
			panelNote = GetPanelNote(NoteType.Regular, beat, generatedPanelIndices); // Note: Currently ignoring hold notes due to rules that prevent arrows from getting generated in some cases (ex: right foot is holding up, left foot can only alternate between left and down for 4 notes in the default ruleset)

			counter.IncrementCounter();
			generatorState.AlternateFoot();
			generatorState.AddPanelToHistory(generatedPanelIndices);

			return true;
		}

		private bool TryGenerateMine(GeneratorInput generatorInput, int beat, out PanelNote panelNote)
		{
			if (settings.disableMines)
			{
				panelNote = null;
				return false;
			}

			if (!mineGenerator.TryGeneratePanel(generatorInput, settings.mineRules, out var generatedPanelIndices))
			{
				panelNote = null;
				return false;
			}

			RemovePanelFromAvailablePanels(generatorInput.availablePanels, generatedPanelIndices);
			panelNote = GetPanelNote(NoteType.Mine, beat, generatedPanelIndices);

			return true;
		}

		private void RemovePanelFromAvailablePanels(bool[,] availablePanels, int[] panelIndices)
		{
			availablePanels.SetValue(false, panelIndices);
		}

		private PanelNote GetPanelNote(NoteType noteType, int beat, int[] generatedPanelIndices)
		{
			var panel = PanelConfigUtil.GetNote(generatedPanelIndices);
			var note = new Note(noteType, beat);
			return new PanelNote(panel, note);
		}
	}
}
