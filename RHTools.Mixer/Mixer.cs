using RHTools.Mixer.Rules;
using RHTools.Mixer.Utils;
using RHTools.Serialization;
using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Mixer
{
	public class Mixer
	{
		private string rhcPath;
		private RhcFile rhcFile;

		public Mixer(string rhcPath)
		{
			this.rhcPath = rhcPath;
			rhcFile = IBinarySerializableExtensions.Deserialize(rhcPath, RhcFile.Deserialize);
		}

		public void Mix(bool[,] panelConfig)
		{
			var layer = rhcFile.layers.layers.First();
			var beatNotesList = NoteConverter.ConvertToBeatNotes(layer.notes);

			var mixedNotes = MixBeatNotes(beatNotesList, panelConfig);

			var rhNotes = NoteConverter.ConvertToRhNotes(mixedNotes);
			layer.notes = rhNotes;

			UpdatePanelConfig(layer, rhNotes);

			var rhDir = Path.GetDirectoryName(rhcPath);
			SaveMixedRhc(rhDir);
			SyncMixedRhcToCache(rhDir);
		}

		private void UpdatePanelConfig(Layer layer, Dictionary<NoteFlags, List<Note>> rhNotes)
		{
			layer.panelConfig = 0;
			foreach (var noteFlags in rhNotes.Keys)
				layer.panelConfig |= noteFlags;
		}

		private void SaveMixedRhc(string rhDir)
		{
			rhcFile.chartName = "Mixed";

			var newGuid = RhGuid.NewGuid();
			rhcFile.rhcGuid = newGuid;
			var newPath = Path.ChangeExtension(Path.Combine(rhDir, newGuid.ToString()), ".rhc");
			rhcFile.SerializeToFile(newPath);
		}

		private void SyncMixedRhcToCache(string rhDir)
		{
			var cachePath = Path.Combine(rhDir, "cache");
			var cacheFile = IBinarySerializableExtensions.Deserialize(cachePath, CacheFile.Deserialize);

			var sync = new RhcSynchronizer(cacheFile, rhcFile);
			sync.Sync();

			cacheFile.SerializeToFile(cachePath);
		}

		public List<List<PanelNote>> MixBeatNotes(List<BeatNotes> beatNotesList, bool[,] panelConfig)
		{
			var panelNotesList = new List<List<PanelNote>>();

			var beatMixer = new BeatMixer(panelConfig);
			foreach (var beatNotes in beatNotesList)
			{
				var panelNotes = beatMixer.MixBeat(beatNotes.notes, new List<Rule>());
				panelNotesList.Add(panelNotes);
			}

			return panelNotesList;
		}
	}
}
