﻿using System;
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
        int currMoveIndex = 0;

        public void GeneratePawnMoves() { }

        public void GenerateKnightMoves(System.Span<Move> moves) 
        {
            ulong knights;

            if (gameState.IsWhiteTurn) { knights = board.WhiteKnights; }

            else { knights = board.BlackKnights; }

            ulong moveMask = board.EnemyPieces & board.EmptySquares; /// Masque des cases où il n'y a pas de pièce alliée

            while (knights != 0)
            {
                int knightSquare = BitboardUtility.PopLSB(ref knights); /// Renvoie l'index de la case du plus petit bit ou il y a un cavalier, et met le bit à 0
                ulong moveSquares = BitboardUtility.KnightAttacks[knightSquare] & moveMask; /// Intersection des cases atteignables par le cavalier et les cases non occupées par des pièces alliées

                while (moveSquares != 0)
                {
                    int targetSquare = BitboardUtility.PopLSB(ref moveSquares); /// Renvoie l'index de la case du plus petit bit et met le bit à 0
                    moves[currMoveIndex++] = new Move(knightSquare, targetSquare); /// Crée le coup correspondant
                }
            }
        }

        public void GenerateSlidingMoves(System.Span<Move> moves)
        {
            ulong orthoSliders;
            ulong diagonalSliders;

            if (gameState.IsWhiteTurn) { orthoSliders = board.WhiteRooks | board.WhiteQueens; diagonalSliders = board.WhiteBishops | board.WhiteQueens; }

            else { orthoSliders = board.BlackRooks | board.BlackQueens; diagonalSliders = board.BlackBishops | board.BlackQueens; }

            ulong moveMask = board.EnemyPieces & board.EmptySquares; /// Masque des cases où il n'y a pas de pièce alliée

            while (orthoSliders != 0)
            {
                int startSquare = BitboardUtility.PopLSB(ref orthoSliders); /// Renvoie l'index de la case du plus petit bit ou il y a un cavalier, et met le bit à 0
                ulong moveSquares = BitboardUtility.KnightAttacks[startSquare] & moveMask; /// Intersection des cases atteignables par le cavalier et les cases non occupées par des pièces alliées

                while (moveSquares != 0)
                {
                    int targetSquare = BitboardUtility.PopLSB(ref moveSquares); /// Renvoie l'index de la case du plus petit bit et met le bit à 0
                    moves[currMoveIndex++] = new Move(startSquare, targetSquare); /// Crée le coup correspondant
                }
            }

            while (diagonalSliders != 0)
            {
                int startSquare = BitboardUtility.PopLSB(ref diagonalSliders); /// Renvoie l'index de la case du plus petit bit ou il y a un cavalier, et met le bit à 0
                ulong moveSquares = BitboardUtility.KnightAttacks[startSquare] & moveMask; /// Intersection des cases atteignables par le cavalier et les cases non occupées par des pièces alliées

                while (moveSquares != 0)
                {
                    int targetSquare = BitboardUtility.PopLSB(ref moveSquares); /// Renvoie l'index de la case du plus petit bit et met le bit à 0
                    moves[currMoveIndex++] = new Move(startSquare, targetSquare); /// Crée le coup correspondant
                }
            }
        }


    }
}
