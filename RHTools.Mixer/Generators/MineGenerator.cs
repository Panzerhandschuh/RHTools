using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Mixer.Generators
{
    public class MineGenerator : PanelGenerator
    {
        public override bool TryGeneratePanel(GeneratorInput generatorInput, out int[] generatedPanelIndices)
        {
            return TryGetRandomPanel(generatorInput.availablePanels, out generatedPanelIndices);
        }
    }
}
