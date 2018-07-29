using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	public class MaxRepetitionsEveryPressRule : Rule
	{
		private readonly int max;

		/// <summary>
		/// Limits the amount of times a particular panel can be repeated every note
		/// </summary>
		/// <param name="max">A value of 0 will prevent any repetition (ex: left, [anything besides left]).
		/// A value of 1 allows double taps (ex: left, left, [anything besides left]).</param>
		public MaxRepetitionsEveryPressRule(int max)
		{
			this.max = Math.Max(max, 0);
		}

		public override void Filter(bool[,] panelConfig, GeneratorState state)
		{
			var history = state.PanelHistory;
			if (history.Count < 1)
				return;

			var lastPanel = history[history.Count - 1];
			if (max == 0) // Special case
			{
				panelConfig[lastPanel[0], lastPanel[1]] = false;
				return;
			}

			var repetitionCounter = 0;
			for (int i = history.Count - 2; i > 0; i--)
			{
				if (history[i] == lastPanel)
				{
					repetitionCounter++;
					if (repetitionCounter >= max)
						panelConfig[lastPanel[0], lastPanel[1]] = false;
				}
			}
		}
	}
}
