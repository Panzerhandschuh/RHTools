using RHTools.Serialization.RH;
using System.Collections.Generic;

namespace RHTools.Converter
{
	public class RhPackAssets
	{
		public List<RhSongAssets> songAssetList;
		public RhGuid pngGuid;
		public RhgFile rhgFile;

		public RhPackAssets()
		{
			songAssetList = new List<RhSongAssets>();
		}
	}
}