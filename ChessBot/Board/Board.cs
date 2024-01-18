using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBot.Board
{
    public class Board
    {
        public ulong WhitePawns { get; set; }
        public ulong WhiteKnights { get; set; }
        public ulong WhiteBishops { get; set; }
        public ulong WhiteRooks { get; set; }
        public ulong WhiteQueens { get; set; }
        public ulong WhiteKing { get; set; }

        public ulong BlackPawns { get; set; }
        public ulong BlackKnights { get; set; }
        public ulong BlackBishops { get; set; }
        public ulong BlackRooks { get; set; }
        public ulong BlackQueens { get; set; }
        public ulong BlackKing { get; set; }

        public ulong WhitePieces;
        public ulong BlackPieces;
        public ulong AllPieces;
        public ulong EmptySquares;

        public Board() 
        {
            InitalizeBoard();
        }

        public void InitalizeBoard()
        {
            WhitePawns = 0b0000000000000000000000000000000000000000000000001111111100000000UL;
            WhiteKnights = 0b0000000000000000000000000000000000000000000000000000000001000010UL;
            WhiteBishops = 0b0000000000000000000000000000000000000000000000000000000000100100UL;
            WhiteRooks = 0b0000000000000000000000000000000000000000000000000000000010000001UL;
            WhiteQueens = 0b0000000000000000000000000000000000000000000000000000000000001000UL;
            WhiteKing = 0b0000000000000000000000000000000000000000000000000000000000010000UL;

            BlackPawns = 0b0000000011111111000000000000000000000000000000000000000000000000UL;
            BlackKnights = 0b0100001000000000000000000000000000000000000000000000000000000000UL;
            BlackBishops = 0b0010010000000000000000000000000000000000000000000000000000000000UL;
            BlackRooks = 0b1000000100000000000000000000000000000000000000000000000000000000UL;
            BlackQueens = 0b0000100000000000000000000000000000000000000000000000000000000000UL;
            BlackKing = 0b0001000000000000000000000000000000000000000000000000000000000000UL;
        }

        public void UpdateExtraBitboards()
        {
            WhitePieces = WhitePawns | WhiteKnights | WhiteBishops | WhiteRooks | WhiteQueens | WhiteKing;
            BlackPieces = BlackPawns | BlackKnights | BlackBishops | BlackRooks | BlackQueens | BlackKing;
            AllPieces = WhitePieces | BlackPieces;
            EmptySquares = ~AllPieces;
        }

    }
}
