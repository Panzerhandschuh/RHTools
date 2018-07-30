using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	public class MaxRepetitionsEveryXNotesRule : Rule
	{
		private readonly int maxRepetitions;
		private readonly int numSteps;

		/// <summary>
		/// Limits the amount of times a particular panel can be repeated every x notes.
		/// Example: maxRepetitions = 0, numSteps = 1 - prevents double taps (left, [anything besides left]).
		/// Example: maxRepetitions = 1, numSteps = 1 - allows double taps but not triple taps (left, left, [anything besides left]).
		/// Example: maxRepetitions = 1, numSteps = 2 - prevents repetitive patterns (left, right, left, right [anything besides left]).
		/// </summary>
		public MaxRepetitionsEveryXNotesRule(int maxRepetitions, int numSteps)
		{
			this.maxRepetitions = Math.Max(maxRepetitions, 0);
			this.numSteps = Math.Max(numSteps, 1);
		}

		public override void Filter(bool[,] panelConfig, GeneratorState state)
		{
			var history = state.PanelHistory;
			if (history.Count < numSteps)
				return;

			var lastPanel = history[history.Count - numSteps];
			if (maxRepetitions == 0) // Special case
			{
				panelConfig[lastPanel[0], lastPanel[1]] = false;
				return;
			}

			var historyIndex = history.Count - (numSteps * 2);
			var historyCounter = 0;
			var repetitionCounter = 0;
			while (historyIndex > 0 && historyCounter < maxRepetitions)
			{
				var panel = history[historyIndex];
				if (panel[0] == lastPanel[0] && panel[1] == lastPanel[1])
					repetitionCounter++;

				historyIndex -= numSteps;
				historyCounter++;
			}

			if (repetitionCounter >= maxRepetitions)
				panelConfig[lastPanel[0], lastPanel[1]] = false;
		}
	}
}
