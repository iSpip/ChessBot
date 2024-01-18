using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ChessBot.Board
{
    public class BitboardUtility
    {
        public static int PopLSB(ref ulong b)
        {
            int i = BitOperations.TrailingZeroCount(b);
            b &= (b - 1);
            return i;
        }

    }
}
