using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class LevelReader
    {
        private ModelLevel levelModel;

        public LevelReader(ModelLevel ml)
        {
            levelModel = ml;

            //Read all maps and place their names in the array of maps
            readAllMaps();
        }

        //Method to get all maps in the map folder and place them in the array maps
        private void readAllMaps()
        {
            levelModel.Maps = Directory.GetFiles(@"map");

            for (int i = 0; i < levelModel.Maps.Length; i++)
            {
                levelModel.Maps[i] = levelModel.Maps[i].Substring(4, levelModel.Maps[i].Count() - 8);
            }
        }

        //Method to read the map and place it in the 2D list
        public void readMapString()
        {
            levelModel.LevelStarted = true;

            //Add the right extensions the level name
            String tempFile = "map/" + levelModel.StartupLevel + ".map";

            //Read the file and place it in the 2D list
            StreamReader sr = new StreamReader(tempFile);

            String line;

            while ((line = sr.ReadLine()) != null)
            {
                if (levelModel.ColumnLenght == 0)
                {
                    levelModel.ColumnLenght = line.Count();
                }

                List<String> templist = new List<String>();

                for (int i = 0; i < line.Count(); i++)
                {
                    String temp = line.Substring(i, 1);
                    templist.Add(temp);
                }

                levelModel.RowLenght++;
                levelModel.StringList.Add(templist);
            }

            levelModel.createArray(levelModel.RowLenght, levelModel.ColumnLenght); 
        }

        public void readMapObject()
        {
            for (int y = 0; y < levelModel.RowLenght; y++)
            {
                List<Tile> row = new List<Tile>();

                for (int x = 0; x < levelModel.ColumnLenght; x++)
                {
                    if (levelModel.StringList[y][x] == "#")
                    {
                        Wall m = new Wall();
                        m.X = x;
                        m.Y = y;
                        row.Add(m);
                    }
                    else if (levelModel.StringList[y][x] == " ")
                    {
                        Floor f = new Floor();
                        f.X = x;
                        f.Y = y;
                        row.Add(f);
                    }
                    else if (levelModel.StringList[y][x] == "@")
                    {
                        Floor f = new Floor();
                        f.X = x;
                        f.Y = y;
                        row.Add(f);

                        //Add player
                        Forklift fork = new Forklift();
                        fork.X = x;
                        fork.Y = y;
                        levelModel.TilesBpt[y,x] = fork;
                        levelModel.Forklift = fork;
                    }
                    else if (levelModel.StringList[y][x] == "o")
                    {
                        Floor f = new Floor();
                        f.X = x;
                        f.Y = y;
                        row.Add(f);

                        //Add box
                        Box b = new Box();
                        b.X = x;
                        b.Y = y;
                        levelModel.TilesBpt[y,x] = b;
                    }
                    else if (levelModel.StringList[y][x] == "x")
                    {
                        levelModel.AmountOfTargets++;
                        Target t = new Target();
                        t.X = x;
                        t.Y = y;
                        row.Add(t);
                    }
                }
                levelModel.Tiles.Add(row);
            }
        }
    }
}
