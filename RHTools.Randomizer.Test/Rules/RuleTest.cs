using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.Randomizer.Utils;
using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Test.Rules
{
	[TestClass]
	public class RuleTest
	{
		protected int[] RandomizeNote(BeatRandomizer randomizer)
		{
			var notes = new List<Note>() { new Note(NoteType.Regular, 0) };
			var randomizedNote = randomizer.RandomizeBeat(notes).Single();

			return PanelConfigUtil.GetPanelIndices(randomizedNote.panel);
		}
	}
}
