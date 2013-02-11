using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Sokoban
{
    class ModelScore
    {
        private int time;
        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        private int moves;
        public int Moves
        {
            get { return moves; }
            set { moves = value; }
        }
    }
}
