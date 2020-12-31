using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI
{
    public static class ArrayExtensions
    {
        public static void AW(this byte[] row, DP data, byte value)
        {
            row[(int)data] = value;
        }

        public static byte GW(this byte[] row, DP data)
        {
            return row[(int)data];
        }
    }
}
