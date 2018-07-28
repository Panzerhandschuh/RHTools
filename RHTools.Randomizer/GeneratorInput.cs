using RHTools.Randomizer.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer
{
    public class GeneratorInput
    {
        public GeneratorState generatorState;
        public bool[,] availablePanels;
		public Random random;

        public GeneratorInput(GeneratorState generatorState, bool[,] availablePanels, Random random)
        {
            this.generatorState = generatorState;
            this.availablePanels = availablePanels;
			this.random = random;
        }
    }
}
