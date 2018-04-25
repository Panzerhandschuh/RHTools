using RHTools.RHLib.RH.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	public class Layers : IBinarySerializable
	{
		public byte unknown1;
		public PadConfiguration padConfig;
		public List<Layer> layers;

		public Layers()
		{
			layers = new List<Layer>();
		}

		public void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}

		public static Layers Deserialize(BinaryReader reader)
		{
			Layers layers = new Layers();

			layers.unknown1 = reader.ReadByte();
			layers.padConfig = (PadConfiguration)reader.ReadByte();
			byte numLayers = reader.ReadByte();
			for (int i = 0; i < numLayers; i++)
				layers.layers.Add(Layer.Deserialize(reader));
			
			return layers;
		}
	}
}
