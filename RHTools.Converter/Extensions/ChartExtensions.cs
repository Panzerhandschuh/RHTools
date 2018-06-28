using RHTools.Serialization.RH;
using RHTools.Serialization.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public static class ChartExtensions
	{
		private const NoteFlags Pad1 = NoteFlags.Pad1Left | NoteFlags.Pad1Down | NoteFlags.Pad1Up | NoteFlags.Pad1Right;
		private const NoteFlags Pad2 = NoteFlags.Pad2Left | NoteFlags.Pad2Down | NoteFlags.Pad2Up | NoteFlags.Pad2Right;

		public static PadConfiguration GetPadConfiguration(this Chart chart)
		{
			switch (chart.chartType)
			{
				case "dance-double":
					return PadConfiguration.Double;
				case "dance-single":
				default:
					return PadConfiguration.Single;
			}
		}

		public static NoteFlags GetPanelConfiguration(this Chart chart)
		{
			switch (chart.chartType)
			{
				case "dance-double":
					return Pad1 | Pad2;
				case "dance-single":
				default:
					return Pad1;
			}
		}

		public static int GetPanelCount(this Chart chart)
		{
			switch (chart.chartType)
			{
				case "dance-double":
					return 8;
				case "dance-single":
				default:
					return 4;
			}
		}
	}
}
