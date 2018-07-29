using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Extensions
{
	public static class RandomExtensions
	{
		public static T NextEnum<T>(this Random random) // TODO: Add where T : Enum (need to upgrade C# version)
		{
			var enumVals = Enum.GetValues(typeof(T));
			var randIndex = random.Next(enumVals.Length);
			return (T)enumVals.GetValue(randIndex);
		}
	}
}
