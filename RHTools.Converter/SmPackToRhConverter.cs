using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Converter
{
	public class SmPackToRhConverter
	{
		private string smPackDir;
		private string rhDir;

		public SmPackToRhConverter(string smPackDir, string rhDir)
		{
			this.smPackDir = smPackDir;
			this.rhDir = rhDir;
		}
	}
}
