using RHTools.Mixer.Generators;
using RHTools.Mixer.Rules;
using RHTools.Mixer.Utils;
using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Mixer
{
	public class BeatMixer
	{
		private StepGenerator stepGenerator;
		private MineGenerator mineGenerator;

		private readonly bool[,] panelConfig;
		private GeneratorState generatorState;

		public BeatMixer(bool[,] panelConfig)
		{
			stepGenerator = new StepGenerator();
			mineGenerator = new MineGenerator();

			this.panelConfig = panelConfig;
			generatorState = new GeneratorState();
		}

		/// <summary>
		/// Generates notes for a single beat
		/// </summary>
		/// <param name="originalNotes">Used by the mixer to distinguish between regular notes, holds, and mines</param>
		/// <returns>Randomized notes for a beat</returns>
		public List<PanelNote> MixBeat(List<Note> originalNotes, List<Rule> rules)
		{
			var panelNotes = new List<PanelNote>();
			var counter = new NoteCounter(panelConfig);

			var beat = originalNotes.First().beat;
			generatorState.UpdateHeldNoteStates(beat);
			var availablePanels = generatorState.GetPanelConfigWithHeldNotesDisabled(panelConfig);

			var generatorInput = new GeneratorInput(generatorState, rules, availablePanels);

			foreach (Note originalNote in originalNotes)
			{
				PanelNote panelNote;
				switch (originalNote.type)
				{
					case NoteType.Regular:
						if (TryGenerateNote(counter, generatorInput, originalNote, false, out panelNote))
							panelNotes.Add(panelNote);
						break;
					case NoteType.Hold:
						if (TryGenerateNote(counter, generatorInput, originalNote, true, out panelNote))
							panelNotes.Add(panelNote);
						break;
					case NoteType.Mine:
						if (TryGenerateMine(generatorInput.availablePanels, originalNote, out panelNote))
							panelNotes.Add(panelNote);
						break;
					default:
						throw new InvalidOperationException();
				}
			}

			return panelNotes;
		}

		private bool TryGenerateNote(NoteCounter counter, GeneratorInput generatorInput, Note originalNote, bool holdNote, out PanelNote panelNote)
		{
			if (counter.IsAtMaxNotes())
			{
				panelNote = null;
				return false;
			}

			if (!stepGenerator.TryGeneratePanel(generatorInput, out var generatedPanelIndices))
			{
				panelNote = null;
				return false;
			}

			panelNote = RemovePanelFromAvailablePanelsAndGetPanelNote(
				generatorInput.availablePanels, generatedPanelIndices, originalNote);

			counter.IncrementCounter();

			if (holdNote)
				generatorState.HoldNoteWithCurrentFoot(panelNote);
			else
				generatorState.AlternateFoot();

			generatorState.AddPanelToHistory(generatedPanelIndices);

			return true;
		}

		private bool TryGenerateMine(bool[,] availablePanels, Note originalNote, out PanelNote panelNote)
		{
			int[] generatedPanelIndices;
			if (!mineGenerator.TryGeneratePanel(availablePanels, out generatedPanelIndices))
			{
				panelNote = null;
				return false;
			}

			panelNote = RemovePanelFromAvailablePanelsAndGetPanelNote(
				availablePanels, generatedPanelIndices, originalNote);

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
