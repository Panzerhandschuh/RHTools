using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Utils
{
	public static class PanelHistoryUtil
	{
		/// <summary>
		/// Gets the last index in the panel history where the foot does not match the specified current foot
		/// </summary>
		public static int GetLastIndexOfLastFoot(IList<PanelHistoryItem> history, Foot currentFoot)
		{
			var indexOfLastNote = GetLastIndexOfLastNote(history);
			return GetIndexOfLastFoot(history, indexOfLastNote, currentFoot);
		}

		/// <summary>
		/// Gets an panel history index before the specified current index that does not match the specified current foot
		/// </summary>
		public static int GetIndexOfLastFoot(IList<PanelHistoryItem> history, int currentIndex, Foot currentFoot)
		{
			while (currentIndex >= 0 && history[currentIndex].foot == currentFoot)
				currentIndex--;

			return currentIndex;
		}

		public static int GetLastIndexOfLastNote(IList<PanelHistoryItem> history)
		{
			return history.Count - 1;
		}

		public static int GetIndexOfLastNote(IList<PanelHistoryItem> history, int currentIndex)
		{
			return currentIndex--;
		}
	}
}
