using RHTools.Randomizer.Extensions;
using RHTools.Randomizer.Utils;
using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer
{
    public class GeneratorState
    {
        public Foot CurrentFoot { get; private set; }
        public ReadOnlyCollection<PanelHistoryItem> PanelHistory
		{
			get
			{
				return panelHistory.AsReadOnly();
			}
		}

		private List<PanelHistoryItem> panelHistory;
		private Dictionary<Foot, PanelNote> heldPanelNotes;
		private Dictionary<Foot, bool> footAlternationQueue; // Used to alternate feet at the end of holds

		public GeneratorState(Random random)
        {
            panelHistory = new List<PanelHistoryItem>();
            CurrentFoot = random.NextEnum<Foot>();
			heldPanelNotes = new Dictionary<Foot, PanelNote>()
			{
				{ Foot.Left, null },
				{ Foot.Right, null },
			};
			footAlternationQueue = new Dictionary<Foot, bool>()
			{
				{ Foot.Left, false },
				{ Foot.Right, false }
			};
		}

		public void AddPanelToHistory(int[] panel)
        {
			var item = new PanelHistoryItem(panel, CurrentFoot);
            panelHistory.Add(item);
        }

		/// <summary>
		/// Holds note and alternates feet
		/// </summary>
		public void HoldNoteWithCurrentFoot(PanelNote panelNote)
		{
			heldPanelNotes[CurrentFoot] = panelNote;
			AlternateFootInternal();
		}

		public void AlternateFoot()
		{
			if (IsAnyNoteHeld())
			{
				footAlternationQueue[CurrentFoot] = true;
				return;
			}

			AlternateFootInternal();
		}

		private bool IsAnyNoteHeld()
		{
			return (heldPanelNotes[Foot.Left] != null || heldPanelNotes[Foot.Right] != null);
		}

		private void AlternateFootInternal()
		{
			CurrentFoot = (CurrentFoot == Foot.Left) ? Foot.Right : Foot.Left;
		}

		/// <summary>
		/// Releases held notes if the specified beat is after the held note expires
		/// </summary>
		public void UpdateHeldNoteStates(int beat)
		{
			UpdateHeldNoteState(beat, Foot.Left);
			UpdateHeldNoteState(beat, Foot.Right);
		}

		private void UpdateHeldNoteState(int beat, Foot foot)
		{
			var panelNote = heldPanelNotes[foot];
			if (panelNote == null)
				return;

			var note = panelNote.note;
			if (beat > note.beat + note.duration)
			{
				heldPanelNotes[foot] = null;
				AlternateFeetIfQueued();
			}
		}

		private void AlternateFeetIfQueued()
		{
			if (footAlternationQueue[CurrentFoot])
			{
				footAlternationQueue[CurrentFoot] = false;
				AlternateFoot();
			}
		}

		public bool[,] GetPanelConfigWithHeldNotesDisabled(bool[,] panelConfig)
		{
			var availablePanels = (bool[,])panelConfig.Clone();

			DisableHeldNote(availablePanels, Foot.Left);
			DisableHeldNote(availablePanels, Foot.Right);

			return availablePanels;
		}

		private void DisableHeldNote(bool[,] availablePanels, Foot foot)
		{
			var panelNote = heldPanelNotes[foot];
			if (panelNote == null)
				return;

			var panelIndices = PanelConfigUtil.GetPanelIndices(panelNote.panel);
			availablePanels[panelIndices[0], panelIndices[1]] = false;
		}

		public int GetNumHeldNotes()
		{
			var count = 0;
			if (heldPanelNotes[Foot.Left] != null)
				count++;
			if (heldPanelNotes[Foot.Right] != null)
				count++;

			return count;
		}
	}
}
