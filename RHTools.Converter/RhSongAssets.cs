﻿using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public class RhSongAssets
	{
		public RhGuid oggGuid;
		public RhGuid pngGuid;
		public RhsFile rhsFile;
		public List<RhcFile> rhcFiles;
	}
}
