using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	public class DisableCenterPanelOverlapRule : Rule
	{
		/// <summary>
		/// Prevents patterns where your left and right feet might overlap when one foot is currently on the center panel (ex: center, down, center)
		/// </summary>
		public DisableCenterPanelOverlapRule() { }

		public override void Filter(bool[,] panelConfig, GeneratorState state)
		{
			var history = state.PanelHistory;
			if (history.Count < 1)
				return;

			var lastNote = history.Last();
			var row = lastNote[0];
			var col = lastNote[1];
			switch (row)
			{
				case 0: // Up
					// Disable center
					panelConfig[1, col] = false;
					break;
				case 1: // Center
					// Disable up and down
					panelConfig[0, col] = false;
					panelConfig[2, col] = false;
					break;
				case 2: // Down
					// Disable center
					panelConfig[1, col] = false;
					break;
			}
		}
	}
}
