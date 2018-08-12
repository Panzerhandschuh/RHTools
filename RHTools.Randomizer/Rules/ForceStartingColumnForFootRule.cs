using RHTools.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	public class ForceStartingColumnForFootRule : Rule
	{
		private Foot foot;
		private int column;

		public ForceStartingColumnForFootRule(Foot foot, int column)
		{
			this.foot = foot;
			this.column = column;
		}

		public override void Filter(bool[,] panelConfig, GeneratorState state)
		{
			if (state.PanelHistory.Any() || // Only apply this rule to the first note generated
				foot != state.CurrentFoot)
				return;

			for (var col = 0; col < PanelConfigUtil.maxColumns; col++)
			{
				if (col == column)
					continue;

				for (var row = 0; row < PanelConfigUtil.maxRows; row++)
					panelConfig[row, col] = false;
			}
		}
	}
}
