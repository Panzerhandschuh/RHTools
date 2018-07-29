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
        public ReadOnlyCollection<int[]> PanelHistory
		{
			get
			{
				return panelHistory.AsReadOnly();
			}
		}

		private List<int[]> panelHistory;
		private Dictionary<Foot, PanelNote> heldPanelNotes;

        public GeneratorState(Random random)
        {
            panelHistory = new List<int[]>();
            CurrentFoot = random.NextEnum<Foot>();
            heldPanelNotes = new Dictionary<Foot, PanelNote>()
            {
                { Foot.Left, null },
                { Foot.Right, null },
            };
        }

        public void AddPanelToHistory(int[] panel)
        {
            panelHistory.Add(panel);
        }

		/// <summary>
		/// Holds note and alternates feet
		/// </summary>
		/// <param name="panelNote"></param>
		public void HoldNoteWithCurrentFoot(PanelNote panelNote)
        {
            heldPanelNotes[CurrentFoot] = panelNote;
			AlternateFootInternal();
        }

        public void AlternateFoot()
		{
			if (IsAnyNoteHeld())
				return;

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
				heldPanelNotes[foot] = null;
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
	}
}
