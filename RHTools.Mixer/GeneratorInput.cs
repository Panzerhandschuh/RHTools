using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Mixer
{
    public class GeneratorInput
    {
        public GeneratorState generatorState;
        public bool[,] availablePanels;

        public GeneratorInput(GeneratorState generatorState, bool[,] availablePanels)
        {
            this.generatorState = generatorState;
            this.availablePanels = availablePanels;
        }
    }
}
