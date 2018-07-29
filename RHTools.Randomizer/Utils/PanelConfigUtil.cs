using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Utils
{
    public static class PanelConfigUtil
    {
		public const int maxRows = 3;
		public const int maxColumns = 6; // 2 pads, 3 columns each

        private static readonly NoteFlags[,] panelIndicesToNoteArray =
        {
            { NoteFlags.Pad1UpLeft, NoteFlags.Pad1Up, NoteFlags.Pad1UpRight, NoteFlags.Pad2UpLeft, NoteFlags.Pad2Up, NoteFlags.Pad2UpRight },
            { NoteFlags.Pad1Left, NoteFlags.Pad1Center, NoteFlags.Pad1Right, NoteFlags.Pad2Left, NoteFlags.Pad2Center, NoteFlags.Pad2Right },
            { NoteFlags.Pad1DownLeft, NoteFlags.Pad1Down, NoteFlags.Pad1DownRight, NoteFlags.Pad2DownLeft, NoteFlags.Pad2Down, NoteFlags.Pad2DownRight }
        };
		
        public static NoteFlags GetNote(int[] panelIndices)
        {
            return (NoteFlags)panelIndicesToNoteArray.GetValue(panelIndices);
        }

		public static int[] GetPanelIndices(NoteFlags noteFlags)
		{
			for (int i = 0; i < panelIndicesToNoteArray.GetLength(0); i++)
			{
				for (int j = 0; j < panelIndicesToNoteArray.GetLength(1); j++)
				{
					if (panelIndicesToNoteArray[i, j] == noteFlags)
						return new int[] { i, j };
				}
			}

			return null;
		}
    }
}
