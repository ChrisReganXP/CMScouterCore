using System;
using System.Linq;

namespace CMScouter.UI
{
    public static class RatingHelper
    {
        public static byte ModifyByte(byte rating, decimal modifier)
        {
            decimal adjusted = rating * modifier;
            if (adjusted > byte.MaxValue)
            {
                return byte.MaxValue;
            }

            if (adjusted < 0)
            {
                return 0;
            }

            return (byte)adjusted;
        }
    }
}
