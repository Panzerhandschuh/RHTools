using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Utils
{
    public class EnumUtil
    {
        public static object GetRandomEnumValue(Type enumType)
        {
            var rand = new Random();
            var enumVals = Enum.GetValues(enumType);
            var randIndex = rand.Next(enumVals.Length);
            return enumVals.GetValue(randIndex);
        }
    }
}
