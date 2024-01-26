using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBot.Board
{
    public class GameState
    {
        public Board Board { get; set; }

        public bool IsWhiteTurn;

        public int ColorToPlay => IsWhiteTurn? 1 : 0;

        public CastleRights CastleRights { get; set; }

        public GameState() 
        {
            Board = new Board();
            IsWhiteTurn = true;
            CastleRights = new CastleRights();
        }



    }
}
