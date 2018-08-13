using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Conditions
{
	public abstract class Condition
	{
		public abstract bool CheckCondition(GeneratorState state);
	}
}
