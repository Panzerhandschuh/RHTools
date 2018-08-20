using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	public class DisableAdjacentRowsRule : DisableAdjacentRowsRuleBase
	{
		/// <summary>
		/// Prevents patterns where your left and right feet might overlap when one foot is currently on the center panel (ex: center, down, center)
		/// </summary>
		public DisableAdjacentRowsRule() { }

		public override void Filter(bool[,] panelConfig, GeneratorState state)
		{
			var history = state.PanelHistory;
			if (history.Count < 1)
				return;

			var lastPanel = history.Last().panel;
			DisableAdjacentRows(panelConfig, lastPanel);
		}
	}
}
