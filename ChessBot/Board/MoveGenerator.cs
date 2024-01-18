using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBot.Board
{
    public class MoveGenerator
    {
        public MoveGenerator() { }

        Board board;

        GameState gameState;

        public void GeneratePawnMoves() { }

        public void GenerateKnightMoves() 
        {
            ulong knights;
            if (gameState.IsWhiteTurn) { knights = board.WhiteKnights; }

            else { knights = board.BlackKnights; }

            while (knights != 0)
            {
                int knightSquare = BitboardUtility.PopLSB(ref knights);
            }
        }


    }
}
