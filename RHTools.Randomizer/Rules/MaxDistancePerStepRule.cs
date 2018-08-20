using RHTools.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	public class MaxDistancePerStepRule : Rule
	{
		private readonly int maxVerticalDistance;
		private readonly int maxHorizontalDistance;

		/// <summary>
		/// Useful for limiting the amount of foot movement between steps.
		/// Example: maxVerticalDistance = 1 - prevents a foot from moving from a panel on the bottom row to a panel on the top row.
		/// </summary>
		public MaxDistancePerStepRule(int maxVerticalDistance, int maxHorizontalDistance)
		{
			this.maxVerticalDistance = Math.Max(maxVerticalDistance, 0);
			this.maxHorizontalDistance = Math.Max(maxHorizontalDistance, 0);
		}

		public override void Filter(bool[,] panelConfig, GeneratorState state)
		{
			var history = state.PanelHistory;
			if (history.Count < 2)
				return;

			var historyPanel = history[history.Count - 2].panel;
			var historyRow = historyPanel[0];
			var historyCol = historyPanel[1];
			for (var row = 0; row < PanelConfigUtil.maxRows; row++)
			{
				for (var col = 0; col < PanelConfigUtil.maxColumns; col++)
				{
					if (row > historyRow + maxHorizontalDistance || col > historyCol + maxVerticalDistance)
						panelConfig[row, col] = false;
				}
			}
		}
	}
}
