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
		public RhgFile Convert(IEnumerable<RhcFile> rhcFiles)
		{
			RhgFile rhgFile = new RhgFile();

			rhgFile.rhgGuid = RhGuid.NewGuid();
			rhgFile.internalGuid = new RhGuid();
			//rhgFile.pngGuid = ;
			//rhgFile.packName = ;
			foreach (RhcFile rhcFile in rhcFiles)
				rhgFile.rhcGuids.Add(rhcFile.rhcGuid);

			return rhgFile;
		}
	}
}
