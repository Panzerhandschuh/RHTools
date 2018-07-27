using RHTools.Serialization.RH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Test.Utils
{
    public static class RandomizerTestUtil
    {
        /// <summary>
        /// Center panel only
        /// </summary>
        public static readonly bool[,] config1Panel = new bool[,]
        {
            { false, false, false, false, false, false },
            { false, true, false, false, false, false },
            { false, false, false, false, false, false }
        };

        /// <summary>
        /// Left and right panels only
        /// </summary>
        public static readonly bool[,] config2Panel = new bool[,]
        {
            { false, false, false, false, false, false },
            { true, false, true, false, false, false },
            { false, false, false, false, false, false }
        };

        public static readonly bool[,] config9Panel = new bool[,]
        {
            { true, true, true, false, false, false },
            { true, true, true, false, false, false },
            { true, true, true, false, false, false }
        };
    }
}
