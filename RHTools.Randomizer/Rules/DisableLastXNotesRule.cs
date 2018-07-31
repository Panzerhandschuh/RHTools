using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	public class DisableLastXNotesRule : Rule
	{
		private readonly int numNotes;

		/// <summary>
		/// Disables the last x notes from available panel options.
		/// This is useful for mines which generally shouldn't be placed right after another note.
		/// Example: numNotes = 1 - left, [mine cannot be left].
		/// Example: numNotes = 2 - left, right, [mine cannot be left or right].
		/// </summary>
		public DisableLastXNotesRule(int numNotes)
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
				panelConfig[panel[0], panel[1]] = false;

				historyIndex--;
				noteCounter++;
			}
		}
	}
}
