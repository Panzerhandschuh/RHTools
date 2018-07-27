using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Generators
{
    public class MineGenerator : PanelGenerator
    {
        public bool TryGeneratePanel(bool[,] availablePanels, out int[] generatedPanelIndices)
        {
            return TryGetRandomPanel(availablePanels, out generatedPanelIndices);
        }
    }
}
