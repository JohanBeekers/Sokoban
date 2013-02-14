using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Sokoban
{
    public class ModelScore
    {
        private String playerName;
        public String PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

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

        //Constructor om default waardes te gebruiken. 
        public ModelScore()
            : this("anonymous", 0, 0)
        {
        }

        //Constructor om direct waardes te geven alle velden.
        public ModelScore(String playerName, int moves, int time)
        {
            this.playerName = playerName;
            this.time = time;
            this.moves = moves;
        }

        /**
         * Methode om 2 ModelScore objecten te vergelijken. 
         * Eerst word naar het aantal moves gekeken. Daarna naar de tijd.
         */
        public int compareTo(ModelScore score)
        {
            int compare = moves.CompareTo(score.moves);
            if (compare == 0)
            {
                compare = time.CompareTo(score.time);
            }
            return compare;
        }
    }
}
