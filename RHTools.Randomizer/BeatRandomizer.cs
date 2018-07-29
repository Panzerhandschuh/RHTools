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
			var counter = new NoteCounter(settings.panelConfig);

			var beat = originalNotes.First().beat;
			generatorState.UpdateHeldNoteStates(beat);
			var availablePanels = generatorState.GetPanelConfigWithHeldNotesDisabled(settings.panelConfig);

			var generatorInput = new GeneratorInput(generatorState, availablePanels, settings.random);

			foreach (Note originalNote in originalNotes)
			{
				PanelNote panelNote;
				switch (originalNote.type)
				{
					case NoteType.Regular:
						if (TryGenerateNote(generatorInput, originalNote, counter, false, out panelNote))
							panelNotes.Add(panelNote);
						break;
					case NoteType.Hold:
						if (TryGenerateNote(generatorInput, originalNote, counter, true, out panelNote))
							panelNotes.Add(panelNote);
						break;
					case NoteType.Mine:
						if (TryGenerateMine(generatorInput, originalNote, out panelNote))
							panelNotes.Add(panelNote);
						break;
					default:
						throw new InvalidOperationException();
				}
			}

			return panelNotes;
		}

		private bool TryGenerateNote(GeneratorInput generatorInput, Note originalNote, NoteCounter counter, bool holdNote, out PanelNote panelNote)
		{
			if (counter.IsAtMaxNotes())
			{
				panelNote = null;
				return false;
			}

			if (!stepGenerator.TryGeneratePanel(generatorInput, settings.rules, out var generatedPanelIndices))
			{
				panelNote = null;
				return false;
			}

			panelNote = RemovePanelFromAvailablePanelsAndGetPanelNote(generatorInput.availablePanels, generatedPanelIndices, originalNote);

			counter.IncrementCounter();

			if (holdNote)
				generatorState.HoldNoteWithCurrentFoot(panelNote);
			else
				generatorState.AlternateFoot();

			generatorState.AddPanelToHistory(generatedPanelIndices);

			return true;
		}

		private bool TryGenerateMine(GeneratorInput generatorInput, Note originalNote, out PanelNote panelNote)
		{
			int[] generatedPanelIndices;
			var rules = new List<Rule>(); // Mines currently have no rules
			if (!mineGenerator.TryGeneratePanel(generatorInput, rules, out generatedPanelIndices))
			{
				panelNote = null;
				return false;
			}

			panelNote = RemovePanelFromAvailablePanelsAndGetPanelNote(generatorInput.availablePanels, generatedPanelIndices, originalNote);

			return true;
		}

		private PanelNote RemovePanelFromAvailablePanelsAndGetPanelNote(bool[,] availablePanels, int[] panelIndices, Note note)
		{
			RemovePanelFromAvailablePanels(availablePanels, panelIndices);
			return GetPanelNote(panelIndices, note);
		}

		private void RemovePanelFromAvailablePanels(bool[,] availablePanels, int[] panelIndices)
		{
			availablePanels.SetValue(false, panelIndices);
		}

		private static PanelNote GetPanelNote(int[] panelIndices, Note note)
		{
			var panel = PanelConfigUtil.GetNote(panelIndices);
			var panelNote = new PanelNote(panel, note);
			return panelNote;
		}
	}
}
