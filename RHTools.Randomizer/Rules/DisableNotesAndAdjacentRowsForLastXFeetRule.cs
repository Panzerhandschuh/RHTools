using RHTools.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	public class DisableNotesAndAdjacentRowsForLastXFeetRule : DisableAdjacentRowsRuleBase
	{
		private readonly int numNotes;

		/// <summary>
		/// Disables the last x notes from available panel options.
		/// This is useful for mines which generally shouldn't be placed right after another note.
		/// This rule applies to foot history rather than note history, which means the last foot positions will be taken into account instead of the last note positions.
		/// Example: numNotes = 1 - left, [mine cannot be left].
		/// Example: numNotes = 2 - left, right, [mine cannot be left or right].
		/// </summary>
		public DisableNotesAndAdjacentRowsForLastXFeetRule(int numNotes)
		{
			this.numNotes = numNotes;
		}

		public override void Filter(bool[,] panelConfig, GeneratorState state)
		{
			var history = state.PanelHistory;
			var historyIndex = PanelHistoryUtil.GetLastIndexOfLastNote(history);
			var noteCounter = 0;
			while (historyIndex >= 0 && noteCounter < numNotes)
			{
				var historyItem = history[historyIndex];
				var panel = historyItem.panel;
				var row = panel[0];
				var col = panel[1];

				panelConfig[row, col] = false;
				DisableAdjacentRows(panelConfig, row, col);

				noteCounter++;
				historyIndex = PanelHistoryUtil.GetIndexOfLastFoot(history, historyIndex, historyItem.foot);
			}
		}
	}
}
