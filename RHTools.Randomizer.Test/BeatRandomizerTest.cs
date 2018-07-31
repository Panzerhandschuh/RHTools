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
			var settings = GetRandomizerSettings(RandomizerTestUtil.config9Panel);
			var randomizer = new BeatRandomizer(settings);
			var notes = GenerateNotes(4);
			var randomizedNotes = randomizer.RandomizeBeat(notes);
		}

		[TestMethod]
		public void CannotGenerateDuplicateNotes()
		{
			var settings = GetRandomizerSettings(RandomizerTestUtil.config9Panel);
			var randomizer = new BeatRandomizer(settings);
			var notes = GenerateNotes(4);
			var randomizedNotes = randomizer.RandomizeBeat(notes);
			var distinctNotes = randomizedNotes.Select(x => x.panel).Distinct();

			Assert.AreEqual(randomizedNotes.Count, distinctNotes.Count());
		}

		[TestMethod]
		public void CannotGenerateMoreThan2Notes()
		{
			var settings = GetRandomizerSettings(RandomizerTestUtil.config9Panel);
			var randomizer = new BeatRandomizer(settings);
			var notes = GenerateNotes(3);
			var randomizedNotes = randomizer.RandomizeBeat(notes);

			Assert.IsTrue(randomizedNotes.Count <= 2);
		}

		[TestMethod]
		public void CannotGenerateMoreNotesThanAvailableInPanelConfig()
		{
			var settings = GetRandomizerSettings(RandomizerTestUtil.config1Panel);
			var randomizer = new BeatRandomizer(settings);
			var notes = GenerateNotes(2);
			var randomizedNotes = randomizer.RandomizeBeat(notes);

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
			var settings = GetRandomizerSettings(RandomizerTestUtil.config2Panel);
			var randomizer = new BeatRandomizer(settings);
			HoldNote(randomizer, 0, 1000);
			HoldNote(randomizer, 0, 1000);

			var randomizedNotes = new List<PanelNote>();
			randomizedNotes.AddRange(PressNote(randomizer, 500));
			randomizedNotes.AddRange(PressNote(randomizer, 999)); // 999 should be the last beat before the note is no longer held
			Assert.AreEqual(0, randomizedNotes.Count);
		}

		[TestMethod]
		public void CanReleaseHeldNotes()
		{
			var settings = GetRandomizerSettings(RandomizerTestUtil.config2Panel);
			var randomizer = new BeatRandomizer(settings);
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

		private BeatRandomizerSettings GetRandomizerSettings(bool[,] panelConfig)
		{
			var settings = new BeatRandomizerSettings();

			settings.panelConfig = panelConfig;
			settings.random = new Random();
			settings.noteRules = new List<Rule>();

			return settings;
		}

		private List<PanelNote> HoldNote(BeatRandomizer randomizer, int beat, int duration)
		{
			var notes = new List<Note>()
			{
				new Note(NoteType.Hold, beat, duration)
			};
			return randomizer.RandomizeBeat(notes);
		}

		private static List<PanelNote> PressNote(BeatRandomizer randomizer, int beat)
		{
			var notes = new List<Note>()
			{
				new Note(NoteType.Regular, beat)
			};
			return randomizer.RandomizeBeat(notes);
		}
	}
}
