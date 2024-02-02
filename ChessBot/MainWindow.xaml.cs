using ChessBot.Board;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameState gameState;
        private int[,] boardMatrix = new int[8, 8];
        private readonly Image[,] pieceImages= new Image[8, 8];
        private (int? row, int? col) selectedSquare = (null, null);
        private double squareSize = 500/8;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            gameState= new GameState();
            DrawBoard(gameState);
        }

        private void InitializeBoard()
        {
            for(int r = 0; r < 8; r++)
            {
                for(int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    pieceImages[r, c] = image;
                    PieceGrid.Children.Add(image);
                }
            }
        }

        private void DrawBoard(GameState gameState)
        {
            boardMatrix = UpdateBoardFromBitboards(gameState);
 
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    int piece = boardMatrix[r, c];
                    Debug.Write(piece + " ");

                    pieceImages[r, c].Source = Images.GetImage(piece);
                }
                Debug.WriteLine("");
            }
        }

        private int[,] UpdateBoardFromBitboards(GameState gameState)
        {
            int[,] board = new int[8, 8];

            for(int r = 0; r < 8; r++) 
            {
                for (int c = 0; c < 8; c++)
                {
                    board[7 - r, c] = gameState.Board.PieceOnSquare(8 * r + c);
                }
            }

            return board;
        }

        private void BoardGrid_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid);
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);

            if (selectedSquare == (null, null))
            {

            }
        }

    }
}
