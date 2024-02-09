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
        private MoveGenerator moveGenerator;
        private int[,] boardMatrix = new int[8, 8];
        private readonly Image[,] pieceImages= new Image[8, 8];
        private int? selectedSquare = null;
        private double squareSize = 500/8;
        private List<Move> validMoves = new List<Move>();


        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            gameState= new GameState();
            moveGenerator = new MoveGenerator();
            
            DrawBoard(gameState);

            GetMoves();
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            GameLoop();
        }

        private async Task GameLoop()
        {
            while (!gameState.IsGameOver) 
            {
                await Task.Delay(100);
            }
        }

        private void GetMoves()
        {
            validMoves.Clear();
            var generatedMoves = moveGenerator.GeneratePossibleMoves(gameState.Board, gameState);
            for (int i = 0; i < generatedMoves.Length; i++)
            {
                validMoves.Add(generatedMoves[i]);
            }
            Debug.WriteLine("Nb of possible moves : " + generatedMoves.Length);
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

                    pieceImages[7-r, c].Source = Images.GetImage(piece);
                }
            }
        }

        private int[,] UpdateBoardFromBitboards(GameState gameState)
        {
            int[,] board = new int[8, 8];

            for(int r = 0; r < 8; r++) 
            {
                for (int c = 0; c < 8; c++)
                {
                    board[7 - r, c] = gameState.Board.PieceOnSquare(8 * (7 - r) + c);
                    Debug.Write(board[7 - r, c] + "");
                }
                Debug.WriteLine("");
            }

            return board;
        }

        private void BoardGrid_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid);
            int row = 7 - (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);

            if (selectedSquare.HasValue) /// Player clicked on a second square
            {
                int startSquare = selectedSquare.Value;
                int endSquare = row * 8 + col;
                selectedSquare = endSquare;
                
                Move move = new Move(startSquare, endSquare);

                if (IsValidMove(move)) /// Move in in valid moves
                {
                    Debug.WriteLine("valid move");
                    gameState.Board.MakeMove(move);
                    DrawBoard(gameState);
                    GetMoves();
                }
                
            }

            else
            {
                selectedSquare = row * 8 + col;
            }
        }

        private bool IsValidMove(Move move)
        {
            if (validMoves.Contains(move))
            {
                return true;
            }

            return false;
        }

    }
}
