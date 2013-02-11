using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Sokoban
{
    class Tile : Image
    {
        private int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        protected void createImage(string naam)
        {
            this.Source = new BitmapImage(new Uri("image/" + naam + ".png", UriKind.Relative));
        }
    }

    class Floor : Tile
    {
        public Floor()
        {
            createImage("floor");
        }
    }

    class Wall : Tile
    {
        public Wall()
        {
            createImage("wall");
        }
    }


    class Target : Tile
    {
        public Target()
        {
            createImage("target");
        }
    }

    class TileBpt : Image
    {
        private int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        protected void createImage(string naam)
        {
            this.Source = new BitmapImage(new Uri("image/" + naam + ".png", UriKind.Relative));
        }
    }

    class Box : TileBpt
    {
        public Box()
        {
            createImage("box");
        }
    }

    class BoxHit : TileBpt
    {
        public BoxHit()
        {
            createImage("box_hit");
        }
    }

    class Forklift : TileBpt
    {
        public Forklift()
        {
            createImage("forklift");
        }
    }
}
