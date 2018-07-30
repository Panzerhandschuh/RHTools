using RHTools.Randomizer.Rules;
using RHTools.Randomizer.Utils;
using RHTools.Serialization;
using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer
{
	/// <summary>
	/// Randomizes a chart (RHC)
	/// </summary>
	public class RhcRandomizer
	{
		private readonly RandomizerSettings settings;
		private RhcFile rhcFile;

		public RhcRandomizer(RandomizerSettings settings)
		{
			this.settings = settings;
			rhcFile = IBinarySerializableExtensions.Deserialize(settings.rhPath, RhcFile.Deserialize);
		}

		public void Randomize()
		{
			var layer = rhcFile.layers.layers.First();
			var beatNotesList = NoteConverter.ConvertToBeatNotes(layer.notes);

			var randomizedNotes = RandomizeBeatNotes(beatNotesList);

			var rhNotes = NoteConverter.ConvertToRhNotes(randomizedNotes);
			layer.notes = rhNotes;

			UpdatePanelConfig(layer, rhNotes);

			var rhDir = Path.GetDirectoryName(settings.rhPath);
			SaveRandomizedRhc(rhDir);
			SyncRandomizedRhcToCache(rhDir);
		}

		private List<List<PanelNote>> RandomizeBeatNotes(List<BeatNotes> beatNotesList)
		{
			var panelNotesList = new List<List<PanelNote>>();

			var beatRandomizer = new BeatRandomizer(settings);
			foreach (var beatNotes in beatNotesList)
			{
				var panelNotes = beatRandomizer.RandomizeBeat(beatNotes.notes);
				if (panelNotes.Any())
					panelNotesList.Add(panelNotes);
			}

			return panelNotesList;
		}

		private void UpdatePanelConfig(Layer layer, Dictionary<NoteFlags, List<Note>> rhNotes)
		{
			layer.panelConfig = 0;
			foreach (var noteFlags in rhNotes.Keys)
				layer.panelConfig |= noteFlags;
		}

		private void SaveRandomizedRhc(string rhDir)
		{
			rhcFile.chartName = "Randomizer";

			var newGuid = RhGuid.NewGuid();
			rhcFile.rhcGuid = newGuid;
			var newPath = Path.ChangeExtension(Path.Combine(rhDir, newGuid.ToString()), ".rhc");
			rhcFile.SerializeToFile(newPath);
		}

		private void SyncRandomizedRhcToCache(string rhDir)
		{
			var cachePath = Path.Combine(rhDir, "cache");
			var cacheFile = IBinarySerializableExtensions.Deserialize(cachePath, CacheFile.Deserialize);

			var sync = new RhcSynchronizer(cacheFile, rhcFile);
			sync.Sync();

			cacheFile.SerializeToFile(cachePath);
		}
	}
}
