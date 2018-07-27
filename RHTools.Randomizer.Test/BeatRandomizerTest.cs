using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.Randomizer.Rules;
using RHTools.Randomizer.Test.Utils;
using RHTools.Serialization.RH;

namespace RHTools.Randomizer.Test
{
	[TestClass]
	public class BeatRandomizerTest
	{
		[TestMethod]
		public void TestBeatRandomizer()
		{
			var randomizer = new BeatRandomizer(RandomizerTestUtil.config9Panel);
			var notes = GenerateNotes(4);
			var randomizedNotes = randomizer.RandomizeBeat(notes, new List<Rule>());
		}

		[TestMethod]
		public void CannotGenerateDuplicateNotes()
		{
			var randomizer = new BeatRandomizer(RandomizerTestUtil.config9Panel);
			var notes = GenerateNotes(4);
			var randomizedNotes = randomizer.RandomizeBeat(notes, new List<Rule>());
			var distinctNotes = randomizedNotes.Select(x => x.panel).Distinct();

			Assert.AreEqual(randomizedNotes.Count, distinctNotes.Count());
		}

		[TestMethod]
		public void CannotGenerateMoreThan2Notes()
		{
			var randomizer = new BeatRandomizer(RandomizerTestUtil.config9Panel);
			var notes = GenerateNotes(3);
			var randomizedNotes = randomizer.RandomizeBeat(notes, new List<Rule>());

			Assert.IsTrue(randomizedNotes.Count <= 2);
		}

		[TestMethod]
		public void CannotGenerateMoreNotesThanAvailableInPanelConfig()
		{
			var randomizer = new BeatRandomizer(RandomizerTestUtil.config1Panel);
			var notes = GenerateNotes(2);
			var randomizedNotes = randomizer.RandomizeBeat(notes, new List<Rule>());

			Assert.IsTrue(randomizedNotes.Count <= 1);
		}

		/// <summary>
		/// Generates notes at beat 0
		/// </summary>
		public static List<Note> GenerateNotes(int numNotes)
		{
			var notes = new List<Note>();

			for (int i = 0; i < numNotes; i++)
				notes.Add(new Note(NoteType.Regular, 0));

			return notes;
		}

		[TestMethod]
		public void CannotGenerateNoteWhenAllAvailableNotesAreHeld()
		{
			var randomizer = new BeatRandomizer(RandomizerTestUtil.config2Panel);
			HoldNote(randomizer, 0, 1000);
			HoldNote(randomizer, 0, 1000);

			var randomizedNotes = new List<PanelNote>();
			randomizedNotes.AddRange(PressNote(randomizer, 500));
			randomizedNotes.AddRange(PressNote(randomizer, 1000)); // Previous notes should still be held, even on the beat at the end of the hold's duration
			Assert.AreEqual(0, randomizedNotes.Count);
		}

		[TestMethod]
		public void CanReleaseHeldNotes()
		{
			var randomizer = new BeatRandomizer(RandomizerTestUtil.config2Panel);
			HoldNote(randomizer, 0, 1000);
			HoldNote(randomizer, 0, 1000);

			var randomizedNotes = PressNote(randomizer, 1001); // Held notes should be released after beat 1000
			Assert.AreEqual(1, randomizedNotes.Count);
		}

		[TestMethod]
		public void CannotAlternateFeetWithOnePanelHeld()
		{
			throw new NotImplementedException();
		}

		private List<PanelNote> HoldNote(BeatRandomizer randomizer, int beat, int duration)
		{
			var notes = new List<Note>()
			{
				new Note(NoteType.Hold, beat, duration)
			};
			return randomizer.RandomizeBeat(notes, new List<Rule>());
		}

		private static List<PanelNote> PressNote(BeatRandomizer randomizer, int beat)
		{
			var notes = new List<Note>()
			{
				new Note(NoteType.Regular, beat)
			};
			return randomizer.RandomizeBeat(notes, new List<Rule>());
		}
	}
}
