using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.Serialization
{
	public interface IBinarySerializable
	{
		void Serialize(BinaryWriter writer);
	}
}
