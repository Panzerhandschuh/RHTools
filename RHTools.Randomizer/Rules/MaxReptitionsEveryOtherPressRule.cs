using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	// TODO: Can generalize this rule with the MaxRepetitionsEveryPressRule by adding an "every other x" parameter
	public class MaxReptitionsEveryOtherPressRule : Rule
	{
		private readonly int max;

		/// <summary>
		/// Limits the amount of times a particular panel can be repeated every other note
		/// </summary>
		/// <param name="max">A value of 0 will prevent repetitions every other note (ex: left, right, [anything besides left]).
		/// A value of 1 will allow 1 repetition (ex: left, right, left, right, [anything besides left]).</param>
		public MaxReptitionsEveryOtherPressRule(int max)
		{
			this.max = Math.Max(max, 0);
		}

		public override void Filter(bool[,] panelConfig, GeneratorState state)
		{
			var history = state.PanelHistory;
			if (history.Count < 2)
				return;

			var secondToLastPanel = history[history.Count - 2];
			if (max == 0) // Special case
			{
				panelConfig[secondToLastPanel[0], secondToLastPanel[1]] = false;
				return;
			}

			var repetitionCounter = 0;
			int[] repeatedPanel = null;
			for (int i = history.Count - 4; i > 0; i -= 2)
			{
				if (history[i] == repeatedPanel)
				{
					repetitionCounter++;
					if (repetitionCounter >= max)
						panelConfig[repeatedPanel[0], repeatedPanel[1]] = false;
				}
			}
		}
	}
}
