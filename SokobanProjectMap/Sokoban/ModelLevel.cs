using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class ModelLevel
    {
        //True if map opend
        private Boolean levelStarted = false;
        public Boolean LevelStarted
        {
            get { return levelStarted; }
            set { levelStarted = value; }
        }

        //Contains the level that is choosen to play
        private String startupLevel;
        public String StartupLevel
        {
            get { return startupLevel; }
            set { startupLevel = value; }
        }

        //Contains all levels in the map folder
        private String[] maps;
        public String[] Maps
        {
            get { return maps; }
            set { maps = value; }
        }

        //Contains the choosen map characters as a String
        private List<List<String>> stringList = new List<List<String>>();
        public List<List<String>> StringList
        {
            get { return stringList; }
            set { stringList = value; }
        }

        //Contains the amount of rows
        private int rowLenght;
        public int RowLenght
        {
            get { return rowLenght; }
            set { rowLenght = value; }
        }

        //Contains the amount of columns
        private int columnLenght;
        public int ColumnLenght
        {
            get { return columnLenght; }
            set { columnLenght = value; }
        }

        //Contains the 2D list which contains the Floor and Wall objects
        private List<List<Tile>> tiles = new List<List<Tile>>();
        internal List<List<Tile>> Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }


        //Contains the 2D list which contains the Target, Box and player objects
        private TileBpt[,] tilesBpt;

        public void createArray(int i, int j)
        {
            tilesBpt = new TileBpt[i, j];
        }

        internal TileBpt[,] TilesBpt
        {
            get { return tilesBpt; }
            set { tilesBpt = value; }
        }
        
        private Forklift forklift;
        internal Forklift Forklift
        {
            get { return forklift; }
            set { forklift = value; }
        }

        private int cellSize = 40;
        public int CellSize
        {
            get { return cellSize; }
        }

        private int infoGridWidth = 200;
        public int InfoGridWidth
        {
            get { return infoGridWidth; }
        }

        private int amountOfTargets;
        public int AmountOfTargets
        {
            get { return amountOfTargets; }
            set { amountOfTargets = value; }
        }

        private int amountOfTargetsDone;
        public int AmountOfTargetsDone
        {
            get { return amountOfTargetsDone; }
            set { amountOfTargetsDone = value; }
        }

    }
}
