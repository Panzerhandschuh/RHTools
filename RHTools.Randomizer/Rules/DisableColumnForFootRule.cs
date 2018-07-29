using RHTools.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	public class DisableColumnForFootRule : Rule
	{
		private readonly Foot foot;
		private readonly int column;

		public DisableColumnForFootRule(Foot foot, int column)
		{
			this.foot = foot;
			this.column = Math.Min(Math.Max(column, 0), PanelConfigUtil.maxColumns - 1);
		}

		public override void Filter(bool[,] panelConfig, GeneratorState state)
		{
			if (foot != state.CurrentFoot)
				return;

			for (int row = 0; row < PanelConfigUtil.maxRows; row++)
				panelConfig[row, column] = false;
		}
	}
}
