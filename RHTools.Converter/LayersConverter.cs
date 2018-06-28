using RHTools.Serialization.RH;
using RHTools.Serialization.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public class LayersConverter
	{
		public Layers Convert(Chart chart)
		{
			var layers = new Layers();
			layers.unknown1 = 0; // Fake data
			layers.padConfig = chart.GetPadConfiguration();
			layers.layers.Add(new LayerConverter().Convert(chart));

			return layers;
		}
	}
}
