using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBot.Board
{
    public class CastleRights
    {
        public bool WKS;
        public bool WQS;
        public bool BKS;
        public bool BQS;

        public CastleRights() 
        {
            this.WKS = true;
            this.WQS = true;
            this.BKS = true;
            this.BQS = true;
        }


    }
}
