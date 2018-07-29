using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
    public abstract class Rule
    {
        public abstract void Filter(bool[,] panelConfig, GeneratorState state);
    }
}
