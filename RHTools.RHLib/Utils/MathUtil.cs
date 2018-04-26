using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.Utils
{
	public static class MathUtil
	{
		/// <summary>
		/// Example: 0101 returns 2
		/// Example: 1110 returns 3
		/// Example: 0000 returns 0 
		/// </summary>
		public static int GetSetBitCount(int i)
		{
			i = i - ((i >> 1) & 0x55555555);
			i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
			return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
		}
	}
}
