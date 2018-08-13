using RHTools.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Conditions
{
	public class ChanceCondition : Condition
	{
		private readonly float chance;

		public ChanceCondition(float chance)
		{
			this.chance = MathUtil.Clamp(chance, 0f, 1f);
		}

		public override bool CheckCondition(GeneratorState state)
		{
			throw new NotImplementedException();
		}
	}
}
