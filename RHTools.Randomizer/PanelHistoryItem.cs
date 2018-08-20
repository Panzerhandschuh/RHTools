using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer
{
	public class PanelHistoryItem
	{
		public int[] panel;
		public Foot foot;

		public PanelHistoryItem() { }

		public PanelHistoryItem(int[] panel, Foot foot)
		{
			this.panel = panel;
			this.foot = foot;
		}
	}
}
