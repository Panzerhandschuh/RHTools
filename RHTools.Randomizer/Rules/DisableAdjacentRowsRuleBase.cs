using RHTools.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	public abstract class DisableAdjacentRowsRuleBase : Rule
	{
		protected void DisableAdjacentRows(bool[,] panelConfig, int[] panel)
		{
			DisableAdjacentRows(panelConfig, panel[0], panel[1]);
		}

		protected void DisableAdjacentRows(bool[,] panelConfig, int row, int col)
		{
			DisableAboveRow(panelConfig, row, col);
			DisableBelowRow(panelConfig, row, col);
		}

		private void DisableAboveRow(bool[,] panelConfig, int row, int col)
		{
			var above = row - 1;
			if (above >= 0)
				panelConfig[above, col] = false;
		}

		private void DisableBelowRow(bool[,] panelConfig, int row, int col)
		{
			var below = row + 1;
			if (below < PanelConfigUtil.maxRows)
				panelConfig[below, col] = false;
		}
	}
}
