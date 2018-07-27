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
	public class Randomizer
	{
		private string rhcPath;
		private RhcFile rhcFile;

		public Randomizer(string rhcPath)
		{
			this.rhcPath = rhcPath;
			rhcFile = IBinarySerializableExtensions.Deserialize(rhcPath, RhcFile.Deserialize);
		}

		public void Randomize(bool[,] panelConfig)
		{
			var layer = rhcFile.layers.layers.First();
			var beatNotesList = NoteConverter.ConvertToBeatNotes(layer.notes);

			var randomizedNotes = RandomizeBeatNotes(beatNotesList, panelConfig);

			var rhNotes = NoteConverter.ConvertToRhNotes(randomizedNotes);
			layer.notes = rhNotes;

			UpdatePanelConfig(layer, rhNotes);

			var rhDir = Path.GetDirectoryName(rhcPath);
			SaveRandomizedRhc(rhDir);
			SyncRandomizedRhcToCache(rhDir);
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

		public List<List<PanelNote>> RandomizeBeatNotes(List<BeatNotes> beatNotesList, bool[,] panelConfig)
		{
			var panelNotesList = new List<List<PanelNote>>();

			var beatRandomizer = new BeatRandomizer(panelConfig);
			foreach (var beatNotes in beatNotesList)
			{
				var panelNotes = beatRandomizer.RandomizeBeat(beatNotes.notes, new List<Rule>());
				panelNotesList.Add(panelNotes);
			}

			return panelNotesList;
		}
	}
}
