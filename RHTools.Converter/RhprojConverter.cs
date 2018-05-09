using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public class RhprojConverter
	{
		public List<RhprojFile> Convert(RhsFile rhsFile, List<RhcFile> rhcFiles)
		{
			List<RhprojFile> rhprojFiles = new List<RhprojFile>();

			foreach (RhcFile rhcFile in rhcFiles)
				rhprojFiles.Add(ConvertRhprojFile(rhsFile, rhcFile));

			return rhprojFiles;
		}

		private static RhprojFile ConvertRhprojFile(RhsFile rhsFile, RhcFile rhcFile)
		{
			RhprojFile rhprojFile = new RhprojFile();

			rhprojFile.unknown1 = 0;
			rhprojFile.rhsFile = rhsFile;
			rhprojFile.unknown2 = new byte[] { 1, 254, 3, 0, 0, 255, 0, 0 };
			rhprojFile.rhcFile = rhcFile;
			rhprojFile.unknown3 = new byte[] { 1, 179, 178, 0, 67, 255, 0, 0, 160, 140, 0, 0, 1, 0, 255 };

			return rhprojFile;
		}
	}
}
