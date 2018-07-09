using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.Mixer.Test.Utils;
using RHTools.Serialization.RH;

namespace RHTools.Mixer.Test
{
    [TestClass]
    public class BeatMixerTest
    {
        [TestMethod]
        public void TestBeatMixer()
        {
            var mixer = new BeatMixer(MixerTestUtil.config9Panel);
            var notes = GenerateNotes(4);
            var mixedNotes = mixer.MixBeat(notes);
        }

        [TestMethod]
        public void CannotGenerateDuplicateNotes()
        {
            var mixer = new BeatMixer(MixerTestUtil.config9Panel);
            var notes = GenerateNotes(4);
            var mixedNotes = mixer.MixBeat(notes);
            var distinctNotes = mixedNotes.Select(x => x.panel).Distinct();

            Assert.AreEqual(mixedNotes.Count, distinctNotes.Count());
        }

        [TestMethod]
        public void CannotGenerateMoreThan2Notes()
        {
            var mixer = new BeatMixer(MixerTestUtil.config9Panel);
            var notes = GenerateNotes(3);
            var mixedNotes = mixer.MixBeat(notes);

            Assert.IsTrue(mixedNotes.Count <= 2);
        }

        [TestMethod]
        public void CannotGenerateMoreNotesThanAvailableInPanelConfig()
        {
            var mixer = new BeatMixer(MixerTestUtil.config1Panel);
            var notes = GenerateNotes(2);
            var mixedNotes = mixer.MixBeat(notes);

            Assert.IsTrue(mixedNotes.Count <= 1);
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
            var mixer = new BeatMixer(MixerTestUtil.config2Panel);
            HoldNote(mixer, 0, 1000);
			HoldNote(mixer, 0, 1000);

			var mixedNotes = new List<PanelNote>();
			mixedNotes.AddRange(PressNote(mixer, 500));
			mixedNotes.AddRange(PressNote(mixer, 1000)); // Previous notes should still be held, even on the beat at the end of the hold's duration
			Assert.AreEqual(0, mixedNotes.Count);
        }

        [TestMethod]
        public void CanReleaseHeldNotes()
        {
			var mixer = new BeatMixer(MixerTestUtil.config2Panel);
			HoldNote(mixer, 0, 1000);
			HoldNote(mixer, 0, 1000);

			var mixedNotes = PressNote(mixer, 1001); // Held notes should be released after beat 1000
			Assert.AreEqual(1, mixedNotes.Count);
		}

		[TestMethod]
		public void CannotAlternateFeetWithOnePanelHeld()
		{
			throw new NotImplementedException();
		}

		private List<PanelNote> HoldNote(BeatMixer mixer, int beat, int duration)
		{
			var notes = new List<Note>()
			{
				new Note(NoteType.Hold, beat, duration)
			};
			return mixer.MixBeat(notes);
		}

		private static List<PanelNote> PressNote(BeatMixer mixer, int beat)
		{
			var notes = new List<Note>()
			{
				new Note(NoteType.Regular, beat)
			};
			return mixer.MixBeat(notes);
		}
	}
}
