using RHTools.Randomizer.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Generators
{
    public abstract class PanelGenerator
    {
		public abstract bool TryGeneratePanel(GeneratorInput generatorInput, List<Rule> rules, out int[] generatedPanelIndices);

		protected bool[,] ApplyRules(List<Rule> rules, GeneratorInput generatorInput)
		{
			var filteredPanelConfig = (bool[,])generatorInput.availablePanels.Clone();
			foreach (var rule in rules)
				rule.Filter(filteredPanelConfig, generatorInput.generatorState);

			return filteredPanelConfig;
		}

		protected bool TryGetRandomPanel(bool[,] panelConfig, Random random, out int[] generatedPanelIndices)
        {
            var availablePanels = FindAvailablePanels(panelConfig);
            if (availablePanels.Count == 0)
            {
                generatedPanelIndices = null;
                return false;
            }

            var randPanelIndex = random.Next(0, availablePanels.Count);

            generatedPanelIndices = availablePanels[randPanelIndex];
            return true;
        }

        private List<int[]> FindAvailablePanels(bool[,] panelConfig)
        {
            var availableNotes = new List<int[]>();

            for (var i = 0; i < panelConfig.GetLength(0); i++)
            {
                for (var j = 0; j < panelConfig.GetLength(1); j++)
                {
                    if ((bool)panelConfig.GetValue(i, j))
                        availableNotes.Add(new int[] { i, j });
                }
            }

            return availableNotes;
        }
    }
}
