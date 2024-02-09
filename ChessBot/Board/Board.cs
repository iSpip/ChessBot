using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public ulong[] piecesList;

        public bool IsWhiteToMove;
        public int MoveColour => IsWhiteToMove ? 0 : 1;

        public ulong[] ColourBitboards = new ulong[2];


        public ulong FriendlyPieces;
        public ulong EnemyPieces;

        public Board() 
        {
            InitalizeBoard();
            UpdateExtraBitboards();
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
            
            IsWhiteToMove = true;


            piecesList = new ulong[]
            {
                WhitePawns,
                WhiteKnights,
                WhiteBishops,
                WhiteRooks,
                WhiteQueens,
                WhiteKing,
                BlackPawns,
                BlackKnights,
                BlackBishops,
                BlackRooks,
                BlackQueens,
                BlackKing
            };
        }

        public void MakeMove(Move move)
        {
            // Get info about move
            int startSquare = move.StartSquare;
            int endSquare = move.TargetSquare;
            int pieceMoved = PieceOnSquare(startSquare);
            int pieceCaptured = PieceOnSquare(endSquare);
            Debug.WriteLine("piece moved : " + pieceMoved);
            Debug.WriteLine("piece captured : " + pieceCaptured);

            piecesList[pieceMoved - 1] = BitboardUtility.ToggleSquares2(piecesList[pieceMoved - 1], startSquare, endSquare);

            if (pieceCaptured != 0)
            {
                piecesList[pieceCaptured - 1] = BitboardUtility.ToggleSquares2(piecesList[pieceCaptured - 1], startSquare, endSquare);
            }
            
            UpdateExtraBitboards();

        }

        public void UndoMove(Move move)
        {
            // Get info about move
            int startSquare = move.StartSquare;
            int endSquare = move.TargetSquare;
            int pieceMoved = PieceOnSquare(startSquare);
            int pieceCaptured = PieceOnSquare(endSquare);
            Debug.WriteLine("piece moved : " + pieceMoved);
            Debug.WriteLine("piece captured : " + pieceCaptured);

            piecesList[pieceMoved - 1] = BitboardUtility.ToggleSquares2(piecesList[pieceMoved - 1], startSquare, endSquare);

            if (pieceCaptured != 0)
            {
                piecesList[pieceCaptured - 1] = BitboardUtility.ToggleSquares2(piecesList[pieceCaptured - 1], startSquare, endSquare);
            }

            UpdateExtraBitboards();
        }

        public void UpdateExtraBitboards()
        {
            WhitePieces = piecesList[0] | piecesList[1] | piecesList[2] | piecesList[3] | piecesList[4] | piecesList[5];
            BlackPieces = piecesList[6] | piecesList[7] | piecesList[8] | piecesList[9] | piecesList[10] | piecesList[11];
            AllPieces = WhitePieces | BlackPieces;
            EmptySquares = ~AllPieces;
            ColourBitboards[0] = WhitePieces;
            ColourBitboards[1] = BlackPieces;
            FriendlyPieces = ColourBitboards[MoveColour];
            EnemyPieces = ColourBitboards[1 - MoveColour];
        }

        public int PieceOnSquare(int square)
        {   
            switch (square)
            {
                case var s when BitboardUtility.KBitIsOne(piecesList[0], s): return 1;
                case var s when BitboardUtility.KBitIsOne(piecesList[1], s): return 2;
                case var s when BitboardUtility.KBitIsOne(piecesList[2], s): return 3;
                case var s when BitboardUtility.KBitIsOne(piecesList[3], s): return 4;
                case var s when BitboardUtility.KBitIsOne(piecesList[4], s): return 5;
                case var s when BitboardUtility.KBitIsOne(piecesList[5], s): return 6;
                case var s when BitboardUtility.KBitIsOne(piecesList[6], s): return 7;
                case var s when BitboardUtility.KBitIsOne(piecesList[7], s): return 8;
                case var s when BitboardUtility.KBitIsOne(piecesList[8], s): return 9;
                case var s when BitboardUtility.KBitIsOne(piecesList[9], s): return 10;
                case var s when BitboardUtility.KBitIsOne(piecesList[10], s): return 11;
                case var s when BitboardUtility.KBitIsOne(piecesList[11], s): return 12;
                default: return 0;
            }
            //if (BitboardUtility.KBitIsOne(WhitePawns, square)) { return 1; }
            //else if (BitboardUtility.KBitIsOne(WhiteKnights, square)) { return 2; }
            //else if (BitboardUtility.KBitIsOne(WhiteBishops, square)) { return 3; }
            //else if (BitboardUtility.KBitIsOne(WhiteRooks, square)) { return 4; }
            //else if (BitboardUtility.KBitIsOne(WhiteQueens, square)) { return 5; }
            //else if (BitboardUtility.KBitIsOne(WhiteKing, square)) { return 6; }

            //else if (BitboardUtility.KBitIsOne(BlackPawns, square)) { return 7; }
            //else if (BitboardUtility.KBitIsOne(BlackKnights, square)) { return 8; }
            //else if (BitboardUtility.KBitIsOne(BlackBishops, square)) { return 9; }
            //else if (BitboardUtility.KBitIsOne(BlackRooks, square)) { return 10; }
            //else if (BitboardUtility.KBitIsOne(BlackQueens, square)) { return 11; }
            //else if (BitboardUtility.KBitIsOne(BlackKing, square)) { return 12; }

            //return 0;

        }

    }
}
