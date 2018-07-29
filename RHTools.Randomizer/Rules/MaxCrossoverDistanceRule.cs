using RHTools.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	// TODO: Might be able to combine functionality with MaxStretchDistanceRule into a single class (the left/right foot logic is just swapped)
	public class MaxCrossoverDistanceRule : Rule
	{
		private readonly int max;

		/// <summary>
		/// Limits the amount of crossover distance between each foot
		/// </summary>
		/// <param name="max">A value of 0 means no crossovers.
		/// A value of 1 means crossovers within one column (ex: right foot right, left foot down, right foot left).</param>
		public MaxCrossoverDistanceRule(int max)
		{
			this.max = Math.Max(max, 0);
		}

		public override void Filter(bool[,] panelConfig, GeneratorState state)
		{
			var history = state.PanelHistory;
			if (history.Count < 1)
				return;

			var startCol = history.Last()[1];
			switch (state.CurrentFoot)
			{
				case Foot.Left:
					FilterLeftFoot(panelConfig, startCol);
					break;
				case Foot.Right:
					FilterRightFoot(panelConfig, startCol);
					break;
			}
		}

		private void FilterLeftFoot(bool[,] panelConfig, int startCol)
		{
			for (int col = startCol + max + 1; col < PanelConfigUtil.maxColumns; col++)
			{
				for (int row = 0; row < PanelConfigUtil.maxRows; row++)
				{
					panelConfig[row, col] = false;
				}
			}
		}

		private void FilterRightFoot(bool[,] panelConfig, int startCol)
		{
			for (int col = 0; col < startCol - max; col++)
			{
				for (int row = 0; row < PanelConfigUtil.maxRows; row++)
				{
					panelConfig[row, col] = false;
				}
			}
		}
	}
}
