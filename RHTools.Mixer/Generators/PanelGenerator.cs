using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Mixer.Generators
{
    public abstract class PanelGenerator
    {
        public abstract bool TryGeneratePanel(GeneratorInput generatorInput, out int[] generatedPanelIndices);

        protected bool TryGetRandomPanel(bool[,] panelConfig, out int[] generatedPanelIndices)
        {
            var availablePanels = FindAvailablePanels(panelConfig);
            if (availablePanels.Count == 0)
            {
                generatedPanelIndices = null;
                return false;
            }

            var rand = new Random();
            var randPanelIndex = rand.Next(0, availablePanels.Count);

            generatedPanelIndices = availablePanels[randPanelIndex];
            return true;
        }

        private List<int[]> FindAvailablePanels(bool[,] panelConfig)
        {
            var availableNotes = new List<int[]>();

            for (int i = 0; i < panelConfig.GetLength(0); i++)
            {
                for (int j = 0; j < panelConfig.GetLength(1); j++)
                {
                    if ((bool)panelConfig.GetValue(i, j))
                        availableNotes.Add(new int[] { i, j });
                }
            }

            return availableNotes;
        }
    }
}
