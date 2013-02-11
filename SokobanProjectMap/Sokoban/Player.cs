using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sokoban
{
    class Player
    {
        private Board board;
        private ModelLevel levelModel;
        private ModelScore scoreModel;
        private MainWindow windowMain;

        private int forkx, forky;
        private int firstBlockX, firstBlockY, secondBlockX, secondBlockY;

        public Player(Board b, ModelLevel m, ModelScore s, MainWindow mw)
        {
            board = b;
            levelModel = m;
            scoreModel = s;
            windowMain = mw;
        }

        public void keyPressed(KeyEventArgs e)
        {
            String key = e.Key.ToString();

            forkx = levelModel.Forklift.X;
            forky = levelModel.Forklift.Y;

            switch (key)
                {
                case "Left":
                        firstBlockX = forkx - 1;
                        firstBlockY = forky;
                        secondBlockX = forkx - 2;
                        secondBlockY = forky;

                        move(180, firstBlockX, firstBlockY, secondBlockX, secondBlockY);
                    break;
                case "Right":
                        firstBlockX = forkx + 1;
                        firstBlockY = forky;
                        secondBlockX = forkx + 2;
                        secondBlockY = forky;

                        move(0, firstBlockX, firstBlockY, secondBlockX, secondBlockY);
                    break;
                case "Up":
                        firstBlockX = forkx;
                        firstBlockY = forky - 1;
                        secondBlockX = forkx;
                        secondBlockY = forky - 2;

                        move(270, firstBlockX, firstBlockY, secondBlockX, secondBlockY);
                    break;
                case "Down":
                        firstBlockX = forkx;
                        firstBlockY = forky + 1;
                        secondBlockX = forkx;
                        secondBlockY = forky + 2;

                        move(90 ,firstBlockX, firstBlockY, secondBlockX, secondBlockY);
                    break;
            }
        }

        private void move(int rotate, int firstBlockX, int firstBlockY, int secondBlockX, int secondBlockY)
        {
            rotateForklift(rotate);

            if (levelModel.TilesBpt[firstBlockY, firstBlockX] == null)
            {
                if (levelModel.Tiles[firstBlockY][firstBlockX] is Floor || levelModel.Tiles[firstBlockY][firstBlockX] is Target)
                {
                    scoreModel.Moves++;

                    levelModel.TilesBpt[firstBlockY , firstBlockX] = levelModel.TilesBpt[forky, forkx];
                    levelModel.TilesBpt[forky, forkx] = null;

                    levelModel.Forklift.X = firstBlockX;
                    levelModel.Forklift.Y = firstBlockY;

                    board.redrawPlayer();
                }
            }
            else
            {
                if (levelModel.TilesBpt[secondBlockY, secondBlockX] == null && !(levelModel.Tiles[secondBlockY][secondBlockX] is Wall))
                {
                    //Up the amount of moves
                    scoreModel.Moves++;

                    if (levelModel.Tiles[secondBlockY][secondBlockX] is Target)
                    {
                        BoxHit bh = new BoxHit();
                        levelModel.TilesBpt[secondBlockY, secondBlockX] = bh;
                        levelModel.TilesBpt[firstBlockY, firstBlockX] = levelModel.TilesBpt[forky, forkx];
                        levelModel.TilesBpt[forky, forkx] = null;
                        board.redrawFloor(firstBlockX,firstBlockY);
                    }
                    else
                    {
                        //dit stukje is orgineel, de if hier boven en de if else hier onder is nieuw voor 
                        //verandere van box image on target
                        levelModel.TilesBpt[secondBlockY, secondBlockX] = levelModel.TilesBpt[firstBlockY, firstBlockX];
                        levelModel.TilesBpt[firstBlockY, firstBlockX] = levelModel.TilesBpt[forky, forkx];
                        levelModel.TilesBpt[forky, forkx] = null;
                    }

                    levelModel.Forklift.X = firstBlockX;
                    levelModel.Forklift.Y = firstBlockY;

                    levelModel.TilesBpt[secondBlockY, secondBlockX].X = secondBlockX;
                    levelModel.TilesBpt[secondBlockY, secondBlockX].Y = secondBlockY;

                    board.redrawPlayerBox(secondBlockX, secondBlockY);


                    //++ amount of targets if box on target
                    if (levelModel.Tiles[secondBlockY][secondBlockX] is Target)
                    {
                        levelModel.AmountOfTargetsDone++;
                    }

                    //-- amount of targets if box leaves target
                    if (levelModel.Tiles[firstBlockY][firstBlockX] is Target)
                    {
                        levelModel.AmountOfTargetsDone--;
                    }

                    //Message if all targets have a box
                    if (levelModel.AmountOfTargetsDone.Equals(levelModel.AmountOfTargets))
                    {
                        windowMain.nextMap();
                    }
                }
            }
        }

        private void rotateForklift(int i)
        {

            RotateTransform angle = new RotateTransform();
            angle.CenterY = 20;
            angle.CenterX = 20;
            angle.Angle = i;
            levelModel.TilesBpt[forky, forkx].RenderTransform = angle;
        }
    }
}
