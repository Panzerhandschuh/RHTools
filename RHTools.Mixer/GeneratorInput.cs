using RHTools.Mixer.Rules;
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
		public List<Rule> rules;
        public bool[,] availablePanels;

        public GeneratorInput(GeneratorState generatorState, List<Rule> rules, bool[,] availablePanels)
        {
            this.generatorState = generatorState;
			this.rules = rules;
            this.availablePanels = availablePanels;
        }
    }
}
