using RHTools.Serialization.RH;
using RHTools.Serialization.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public class LayerConverter
	{
		public Layer Convert(Chart chart)
		{
			var layer = new Layer();

			layer.unknown1 = new byte[] { 0, 0 }; // Fake data
			layer.layerName = "Layer 1";
			layer.isEnabled = true;
			layer.isFake = false;
			layer.unknown2 = new byte[] { 0, 4, 1, 0, 255, 255, 255, 127, 0, 0, 0, 128 }; // Fake data
			layer.panelConfig = chart.GetPanelConfiguration();
			layer.notes = new NoteConverter().Convert(chart);

			return layer;
		}
	}
}
