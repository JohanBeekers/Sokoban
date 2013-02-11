using System;
using System.Collections.Generic;
using System.IO;
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

namespace Sokoban
{
    public class Board : Grid
    {
        private ModelLevel levelModel;

        public Board(ModelLevel ml)
        {
            levelModel = ml;

            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFrom("#FF5EC5F5");

            setGrid();
            drawMap();
            setBp();
        }

        private void setGrid()
        {
            for (int i = 0; i < levelModel.RowLenght; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(levelModel.CellSize);
                this.RowDefinitions.Add(row);
            }

            for (int i = 0; i < levelModel.ColumnLenght; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(levelModel.CellSize);
                this.ColumnDefinitions.Add(col);
            }
        }

        private void drawMap()
        {
            for (int y = 0; y < levelModel.RowLenght; y++)
            {
                for (int x = 0; x < levelModel.ColumnLenght; x++)
                {
                    Tile t = levelModel.Tiles[y][x];
                    t.SetValue(Grid.ColumnProperty, x);
                    t.SetValue(Grid.RowProperty, y);
                    this.Children.Remove(t);
                    this.Children.Add(t);
                }
            }
        }
    
        private void setBp()
        {
            for (int y = 0; y < levelModel.RowLenght; y++)
            {
                for (int x = 0; x < levelModel.ColumnLenght; x++)
                {
                    TileBpt t = levelModel.TilesBpt[y,x];
                    if (t != null)
                    {
                        t.SetValue(Grid.ColumnProperty, x);
                        t.SetValue(Grid.RowProperty, y);
                        this.Children.Remove(t);
                        this.Children.Add(t);
                    }
                }
            }
        }

        public void redrawPlayerBox(int xsecond, int ysecond)
        {
            //update forklift
            redrawPlayer();

            //Update box
            redrawBox(xsecond, ysecond);
        }

        public void redrawPlayer()
        {
            //update forklift
            levelModel.Forklift.SetValue(Grid.ColumnProperty, levelModel.Forklift.X);
            levelModel.Forklift.SetValue(Grid.RowProperty, levelModel.Forklift.Y);
            this.Children.Remove(levelModel.Forklift);
            this.Children.Add(levelModel.Forklift);
        }

        public void redrawBox(int xsecond, int ysecond)
        {
            //Update box
            levelModel.TilesBpt[ysecond, xsecond].SetValue(Grid.ColumnProperty, levelModel.TilesBpt[ysecond, xsecond].X);
            levelModel.TilesBpt[ysecond, xsecond].SetValue(Grid.RowProperty, levelModel.TilesBpt[ysecond, xsecond].Y);
            this.Children.Remove(levelModel.TilesBpt[ysecond, xsecond]);
            this.Children.Add(levelModel.TilesBpt[ysecond, xsecond]);
        }

        public void redrawFloor(int x, int y)
        {
            Tile t = levelModel.Tiles[y][x];
            t.SetValue(Grid.ColumnProperty, x);
            t.SetValue(Grid.RowProperty, y);
            this.Children.Remove(t);
            this.Children.Add(t);
        }

    }
}