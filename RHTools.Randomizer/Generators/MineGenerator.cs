using RHTools.Randomizer.Rules;
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
        public override bool TryGeneratePanel(GeneratorInput generatorInput, List<Rule> rules, out int[] generatedPanelIndices)
        {
			var filteredPanelConfig = ApplyRules(rules, generatorInput);

			return TryGetRandomPanel(filteredPanelConfig, generatorInput.random, out generatedPanelIndices);
        }
    }
}
