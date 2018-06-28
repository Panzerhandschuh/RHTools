using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public class RhgConverter
	{
		public RhgFile Convert(RhGuid pngGuid, string packName, IEnumerable<RhGuid> rhcGuids)
		{
			var rhgFile = new RhgFile();

			rhgFile.rhgGuid = RhGuid.NewGuid();
			rhgFile.internalGuid = new RhGuid();
			rhgFile.pngGuid = pngGuid;
			rhgFile.packName = packName;
			foreach (var rhcFile in rhcGuids)
				rhgFile.rhcGuids.Add(rhcFile);

			return rhgFile;
		}
	}
}
