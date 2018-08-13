using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Conditions
{
	public class MaxNpsCondition : Condition
	{
		private readonly int maxNps;

		public MaxNpsCondition(int maxNps)
		{
			this.maxNps = Math.Max(maxNps, 0);
		}

		public override bool CheckCondition(GeneratorState state)
		{
			throw new NotImplementedException();
		}
	}
}
