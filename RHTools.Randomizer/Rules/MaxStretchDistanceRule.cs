using RHTools.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	public class MaxStretchDistanceRule : Rule
	{
		private readonly int max;

		/// <summary>
		/// Limits the amount of distance between each foot
		/// </summary>
		/// <param name="max"></param>
		public MaxStretchDistanceRule(int max)
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
			for (int col = 0; col < startCol - max; col++)
			{
				for (int row = 0; row < PanelConfigUtil.maxRows; row++)
				{
					panelConfig[row, col] = false;
				}
			}
		}

		private void FilterRightFoot(bool[,] panelConfig, int startCol)
		{
			for (int col = startCol + max + 1; col < PanelConfigUtil.maxColumns; col++)
			{
				for (int row = 0; row < PanelConfigUtil.maxRows; row++)
				{
					panelConfig[row, col] = false;
				}
			}
		}
	}
}
