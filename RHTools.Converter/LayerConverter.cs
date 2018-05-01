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
			Layer layer = new Layer();

			//layer.unknown1 = ;
			layer.layerName = "Layer 1";
			layer.isEnabled = true;
			layer.isFake = false;
			//layer.unknown2 = ;
			layer.panelConfig = chart.GetPanelConfiguration();
			layer.notes = new NoteConverter().Convert(chart);

			return layer;
		}
	}
}
