using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Mixer.Rules
{
    public abstract class Rule
    {
        public abstract bool[,] Filter(bool[,] panelConfig);
    }
}
