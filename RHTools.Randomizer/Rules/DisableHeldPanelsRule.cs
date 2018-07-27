using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Rules
{
    public class DisableHeldPanelsRule : Rule
    {
        private int currentBeat;
        private Dictionary<Foot, Note> heldNotes;

        public DisableHeldPanelsRule(int currentBeat, Dictionary<Foot, Note> heldNotes)
        {
            this.currentBeat = currentBeat;
            this.heldNotes = heldNotes;
        }

        public override bool[,] Filter(bool[,] panelConfig)
        {
            throw new NotImplementedException();
        }
    }
}
