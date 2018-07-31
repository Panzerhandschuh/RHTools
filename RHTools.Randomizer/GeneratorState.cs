using RHTools.Randomizer.Extensions;
using RHTools.Randomizer.Utils;
using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer
{
    public class GeneratorState
    {
        public Foot CurrentFoot { get; private set; }
        public ReadOnlyCollection<int[]> PanelHistory
		{
			get
			{
				return panelHistory.AsReadOnly();
			}
		}

		private List<int[]> panelHistory;

        public GeneratorState(Random random)
        {
            panelHistory = new List<int[]>();
            CurrentFoot = random.NextEnum<Foot>();
        }

        public void AddPanelToHistory(int[] panel)
        {
            panelHistory.Add(panel);
        }

        public void AlternateFoot()
		{
			CurrentFoot = (CurrentFoot == Foot.Left) ? Foot.Right : Foot.Left;
		}
	}
}
