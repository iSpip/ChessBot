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
        public static readonly ulong[] KnightAttacks;
        public static readonly ulong[] OrthoAttacks;
        public static readonly ulong[] DiagAttacks;
        public static readonly ulong[] KingMoves;
        public static readonly ulong[] WhitePawnAttacks;
        public static readonly ulong[] BlackPawnAttacks;

        static BitboardUtility()
        {
            KnightAttacks = new ulong[64];
            OrthoAttacks= new ulong[64];
            DiagAttacks= new ulong[64];
            KingMoves = new ulong[64];
            WhitePawnAttacks = new ulong[64];
            BlackPawnAttacks = new ulong[64];

            (int x, int y)[] orthoDir = { (-1, 0), (0, 1), (1, 0), (0, -1) };
            (int x, int y)[] diagDir = { (-1, -1), (-1, 1), (1, 1), (1, -1) };
            (int x, int y)[] knightJumps = { (-2, -1), (-2, 1), (-1, 2), (1, 2), (2, 1), (2, -1), (1, -2), (-1, -2) };

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    ProcessSquare(x, y);
                }
            }

            void ProcessSquare(int x, int y)
            {
                int squareIndex = y * 8 + x;

                // Knight jumps
                for (int i = 0; i < knightJumps.Length; i++)
                {
                    int knightX = x + knightJumps[i].x;
                    int knightY = y + knightJumps[i].y;
                    if (ValidSquareIndex(knightX, knightY, out int knightTargetSquare))
                    {
                        KnightAttacks[squareIndex] |= 1ul << knightTargetSquare;
                    }
                }

                for (int dirIndex = 0; dirIndex < 4; dirIndex++)
                {
                    // Orthogonal and diagonal directions
                    for (int dst = 1; dst < 8; dst++)
                    {
                        int orthoX = x + orthoDir[dirIndex].x * dst;
                        int orthoY = y + orthoDir[dirIndex].y * dst;
                        int diagX = x + diagDir[dirIndex].x * dst;
                        int diagY = y + diagDir[dirIndex].y * dst;

                        if (ValidSquareIndex(orthoX, orthoY, out int orthoTargetIndex))
                        {
                            if (dst == 1)
                            {
                                KingMoves[squareIndex] |= 1ul << orthoTargetIndex;
                            }

                            OrthoAttacks[squareIndex] |= 1ul << orthoTargetIndex;
                        }

                        if (ValidSquareIndex(diagX, diagY, out int diagTargetIndex))
                        {
                            if (dst == 1)
                            {
                                KingMoves[squareIndex] |= 1ul << diagTargetIndex;
                            }

                            DiagAttacks[squareIndex] |= 1ul << diagTargetIndex;
                        }
                    }

                    

                    // Pawn attacks

                    if (ValidSquareIndex(x + 1, y + 1, out int whitePawnRight))
                    {
                        WhitePawnAttacks[squareIndex] |= 1ul << whitePawnRight;
                    }
                    if (ValidSquareIndex(x - 1, y + 1, out int whitePawnLeft))
                    {
                        WhitePawnAttacks[squareIndex] |= 1ul << whitePawnLeft;
                    }


                    if (ValidSquareIndex(x + 1, y - 1, out int blackPawnAttackRight))
                    {
                        BlackPawnAttacks[squareIndex] |= 1ul << blackPawnAttackRight;
                    }
                    if (ValidSquareIndex(x - 1, y - 1, out int blackPawnAttackLeft))
                    {
                        BlackPawnAttacks[squareIndex] |= 1ul << blackPawnAttackLeft;
                    }


                }
            }

            bool ValidSquareIndex(int x, int y, out int index)
            {
                index = y * 8 + x;
                return x >= 0 && x < 8 && y >= 0 && y < 8;
            }
        }

        public static int PopLSB(ref ulong b)
        {
            int i = BitOperations.TrailingZeroCount(b);
            b &= (b - 1);
            return i;
        }

        public static bool KBitIsOne(ulong bitboard, int k)
        {
            ulong shifted = bitboard >> k;

            return (shifted & 1) == 1;
        }

    }
}
