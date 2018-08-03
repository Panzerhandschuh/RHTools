using RHTools.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	public class DisableLastXNotesAndAdjacentRowsRule : Rule
	{
		private readonly int numNotes;

		/// <summary>
		/// Disables the last x notes from available panel options.
		/// This is useful for mines which generally shouldn't be placed right after another note.
		/// Example: numNotes = 1 - left, [mine cannot be left].
		/// Example: numNotes = 2 - left, right, [mine cannot be left or right].
		/// </summary>
		public DisableLastXNotesAndAdjacentRowsRule(int numNotes)
		{
			this.numNotes = numNotes;
		}

		public override void Filter(bool[,] panelConfig, GeneratorState state)
		{
			var history = state.PanelHistory;
			var historyIndex = history.Count - 1;
			var noteCounter = 0;
			while (historyIndex >= 0 && noteCounter < numNotes)
			{
				var panel = history[historyIndex];
				var row = panel[0];
				var col = panel[1];

				panelConfig[row, col] = false;

				// TODO: Consolidate logic between this class and DisableAdjacentRowsRule
				DisableAboveRow(panelConfig, row, col);
				DisableBelowRow(panelConfig, row, col);

				historyIndex--;
				noteCounter++;
			}
		}

		private static void DisableAboveRow(bool[,] panelConfig, int row, int col)
		{
			var above = row - 1;
			if (above >= 0)
				panelConfig[above, col] = false;
		}

		private static void DisableBelowRow(bool[,] panelConfig, int row, int col)
		{
			var below = row + 1;
			if (below < PanelConfigUtil.maxRows)
				panelConfig[below, col] = false;
		}
	}
}
