using RHTools.RHLib.Serialization;
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
			writer.Write(unknown1);
			writer.Write((byte)padConfig);
			writer.WriteBytePrefixedList(layers);
		}

		public static Layers Deserialize(BinaryReader reader)
		{
			Layers layers = new Layers();

			layers.unknown1 = reader.ReadByte();
			layers.padConfig = (PadConfiguration)reader.ReadByte();
			layers.layers = reader.ReadBytePrefixedList(Layer.Deserialize);
			
			return layers;
		}
	}
}
