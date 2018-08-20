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

			var lastPanelIndex = PanelHistoryUtil.GetLastIndexOfLastNote(history);
			var lastFootIndex = PanelHistoryUtil.GetIndexOfLastFoot(history, lastPanelIndex, history[lastPanelIndex].foot);
			var lastFootPanel = history[lastFootIndex].panel;
			var historyRow = lastFootPanel[0];
			var historyCol = lastFootPanel[1];
			for (var row = 0; row < PanelConfigUtil.maxRows; row++)
			{
				for (var col = 0; col < PanelConfigUtil.maxColumns; col++)
				{
					if (Math.Abs(row - historyRow) > maxHorizontalDistance || Math.Abs(col - historyCol) > maxVerticalDistance)
						panelConfig[row, col] = false;
				}
			}
		}
	}
}
