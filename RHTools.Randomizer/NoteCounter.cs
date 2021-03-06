﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer
{
    public class NoteCounter
    {
        private int counter;
        private int maxNotesForPanelConfig;

        public NoteCounter(bool[,] panelConfig, int maxNotesPerBeat, int numHeldNotes)
        {
            var numPanelsAvailable = panelConfig.Cast<bool>().Count(x => x);
            maxNotesForPanelConfig = Math.Min(maxNotesPerBeat, numPanelsAvailable);
			counter = numHeldNotes;
        }

        public void IncrementCounter()
        {
            counter++;
        }

        public bool IsAtMaxNotes()
        {
            return counter >= maxNotesForPanelConfig;
        }
    }
}
