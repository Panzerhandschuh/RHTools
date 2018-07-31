﻿using RHTools.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
	public class MaxSpanRule : Rule
	{
		private readonly int maxStretchDistance;
		private readonly int maxCrossoverDistance;

		/// <summary>
		/// Limits the amount of stretch and crossover distance between each foot.
		/// Stretch distance indicates how far apart each foot can be.
		/// Crossover distance indicates how far a foot can cross the other foot.
		/// Example: maxStretchDistance = 2 - prevents your right foot from stretching to the 2nd pad while the left foot is on the left side of the 1st pad.
		/// Example: maxCrossoverDistance = 0 - prevents crossovers.
		/// Example: maxCrossoverDistance = 1 - allows crossovers within one panel.
		/// </summary>
		public MaxSpanRule(int maxStretchDistance, int maxCrossoverDistance)
		{
			this.maxStretchDistance = Math.Max(maxStretchDistance, 0);
			this.maxCrossoverDistance = Math.Max(maxCrossoverDistance, 0);
		}

		public override void Filter(bool[,] panelConfig, GeneratorState state)
		{
			var history = state.PanelHistory;
			if (history.Count < 1)
				return;

			var startCol = history.Last()[1]; // Note: This logic currently assumes that the other foot pressed the last note. This will not always be the case if hold notes are implemented (ex: left foot could be held multiple notes ago).
			switch (state.CurrentFoot)
			{
				case Foot.Left:
					FilterLeftOfStartColumn(panelConfig, startCol, maxStretchDistance); // Prevent left foot from stretching too far to the left
					FilterRightOfStartColumn(panelConfig, startCol, maxCrossoverDistance); // Prevent left foot from crossing over too far to the right
					break;
				case Foot.Right:
					FilterRightOfStartColumn(panelConfig, startCol, maxStretchDistance);
					FilterLeftOfStartColumn(panelConfig, startCol, maxCrossoverDistance);
					break;
			}
		}

		private void FilterLeftOfStartColumn(bool[,] panelConfig, int startCol, int maxDistance)
		{
			for (int col = 0; col < startCol - maxDistance; col++)
			{
				for (int row = 0; row < PanelConfigUtil.maxRows; row++)
				{
					panelConfig[row, col] = false;
				}
			}
		}

		private void FilterRightOfStartColumn(bool[,] panelConfig, int startCol, int maxDistance)
		{
			for (int col = startCol + maxDistance + 1; col < PanelConfigUtil.maxColumns; col++)
			{
				for (int row = 0; row < PanelConfigUtil.maxRows; row++)
				{
					panelConfig[row, col] = false;
				}
			}
		}
	}
}