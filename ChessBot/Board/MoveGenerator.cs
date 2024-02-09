﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBot.Board
{
    public class MoveGenerator
    {

        Board board;
        GameState gameState;
        int currMoveIndex = 0;
        public const int MaxMoves = 218;

        public MoveGenerator()
        {
        }

        public Span<Move> GeneratePossibleMoves(Board board, GameState gameState)
        {
            Span<Move> moves = new Move[MaxMoves];
            GeneratePossibleMoves(board, gameState, ref moves);
            return moves;
        }

        public void GeneratePossibleMoves(Board board, GameState gameState, ref Span<Move> moves)
        {
            this.board = board;
            this.gameState = gameState;

            GeneratePawnMoves();
            GenerateKnightMoves(moves);
            GenerateSlidingMoves(moves);
            GenerateKingMoves(moves);

            moves = moves.Slice(0, currMoveIndex);

        }

        public void GeneratePawnMoves() { }

        public void GenerateKnightMoves(System.Span<Move> moves) 
        {
            ulong knights;
            if (gameState.IsWhiteTurn) { knights = board.WhiteKnights; }

            else { knights = board.BlackKnights; }

            ulong moveMask = board.EnemyPieces | board.EmptySquares; /// Masque des cases où il n'y a pas de pièce alliée

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
                ulong moveSquares = BitboardUtility.OrthoAttacks[startSquare] & moveMask; /// Intersection des cases atteignables par le cavalier et les cases non occupées par des pièces alliées

                while (moveSquares != 0)
                {
                    int targetSquare = BitboardUtility.PopLSB(ref moveSquares); /// Renvoie l'index de la case du plus petit bit et met le bit à 0
                    moves[currMoveIndex++] = new Move(startSquare, targetSquare); /// Crée le coup correspondant
                }
            }

            while (diagonalSliders != 0)
            {
                int startSquare = BitboardUtility.PopLSB(ref diagonalSliders); /// Renvoie l'index de la case du plus petit bit ou il y a un cavalier, et met le bit à 0
                ulong moveSquares = BitboardUtility.DiagAttacks[startSquare] & moveMask; /// Intersection des cases atteignables par le cavalier et les cases non occupées par des pièces alliées

                while (moveSquares != 0)
                {
                    int targetSquare = BitboardUtility.PopLSB(ref moveSquares); /// Renvoie l'index de la case du plus petit bit et met le bit à 0
                    moves[currMoveIndex++] = new Move(startSquare, targetSquare); /// Crée le coup correspondant
                }
            }
        }

        public void GenerateKingMoves(System.Span<Move> moves)
        {
            ulong friendlyKingSquare;
            ulong opponentAttackMap;
            if (gameState.IsWhiteTurn) { friendlyKingSquare = board.WhiteKing; }
            else { friendlyKingSquare = board.WhiteKing; }

            //ulong legalMask = ~(opponentAttackMap | board.FriendlyPieces);
            //ulong kingMoves = BitboardUtility.KingMoves[friendlyKingSquare] & legalMask;

            //while (kingMoves != 0)
            //{
            //    int targetSquare = BitboardUtility.PopLSB(ref kingMoves);
            //    moves[currMoveIndex++] = new Move(friendlyKingSquare, targetSquare);
            //}
        }


    }
}
