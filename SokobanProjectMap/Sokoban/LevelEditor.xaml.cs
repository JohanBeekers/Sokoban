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
    /// <summary>
    /// Interaction logic for LevelEditor.xaml
    /// </summary>
    public partial class LevelEditor : UserControl
    {
        private List<List<Tile>> tiles = new List<List<Tile>>();
        private ModelLevel levelModel;
        private LevelReader levelReader;
        private static int minGridWidth = 5, minGridHeight = 5, maxGridWidth = 9, maxGridHeight = 9;

        public LevelEditor(ModelLevel levelModel, LevelReader levelReader)
        {
            InitializeComponent();

            //Fill the list with existing levels
            this.levelModel = levelModel;
            this.levelReader = levelReader;
            mapsListBox.ItemsSource = levelModel.Maps;

            //Fill the list with level tiles.
            tilesListBox.Items.Add(new Wall());
            tilesListBox.Items.Add(new Floor());
            tilesListBox.Items.Add(new Target());
            tilesListBox.Items.Add(new Box());
            tilesListBox.Items.Add(new Forklift());
            tilesListBox.SelectedIndex = 0;

            initGrid();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void initGrid()
        {
            int width = levelGrid.ColumnDefinitions.Count();
            int height = levelGrid.RowDefinitions.Count();

            for (int y = 0; y < height; y++)
            {
                List<Tile> row = new List<Tile>();
                for (int x = 0; x < width; x++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        Wall wall = new Wall();
                        wall.SetValue(Grid.ColumnProperty, x);
                        wall.SetValue(Grid.RowProperty, y);

                        row.Add(wall);
                        levelGrid.Children.Add(wall);
                    }
                    else
                    {
                        Floor floor = new Floor();
                        floor.SetValue(Grid.ColumnProperty, x);
                        floor.SetValue(Grid.RowProperty, y);

                        row.Add(floor);
                        levelGrid.Children.Add(floor);
                    }
                }
                tiles.Add(row);
            }
        }

        private void verticalPlus_Click(object sender, RoutedEventArgs e)
        {
            if (levelGrid.RowDefinitions.Count() >= maxGridHeight)
            {
                return;
            }

            //Add a new row to the grid.
            RowDefinition gridRow = new RowDefinition();
            gridRow.Height = new GridLength(levelModel.CellSize);
            levelGrid.RowDefinitions.Add(gridRow);

            //Get the new total width and height. 
            int width = levelGrid.ColumnDefinitions.Count();
            int height = levelGrid.RowDefinitions.Count();

            //Move the wall row one down in both the tiles array and grid.
            foreach (Tile tile in tiles[height - 2])
            {
                tile.SetValue(Grid.RowProperty, height - 1);
            }
            tiles.Add(tiles[height-2]);

            //Create a new row that will be inserted in the old wall row. 
            List<Tile> row = new List<Tile>();
            for (int i = 0; i < width; i++)
            {
                //Walls left and right. The rest is floor.
                if (i == 0 || i == width - 1)
                {
                    Wall wall = new Wall();
                    wall.SetValue(Grid.ColumnProperty, i);
                    wall.SetValue(Grid.RowProperty, height-2);

                    row.Add(wall);
                    levelGrid.Children.Add(wall);
                }
                else
                {
                    Floor floor = new Floor();
                    floor.SetValue(Grid.ColumnProperty, i);
                    floor.SetValue(Grid.RowProperty, height-2);

                    row.Add(floor);
                    levelGrid.Children.Add(floor);
                }
            }
            tiles[height - 2] = row;
        }

        private void verticalMin_Click(object sender, RoutedEventArgs e)
        {
            if (levelGrid.RowDefinitions.Count() <= minGridHeight)
            {
                return;
            }

            //Get the current total width and height. 
            int width = levelGrid.ColumnDefinitions.Count();
            int height = levelGrid.RowDefinitions.Count();

            //Remove the row above the wall row from the grid. 
            foreach (Tile tile in tiles[height - 2])
            {
                levelGrid.Children.Remove(tile);
            }
            //Move the wall row one row up.
            foreach (Tile tile in tiles[height - 1])
            {
                tile.SetValue(Grid.RowProperty, height-2);
            }
            //Move the row in the tiles array and remove the old one.
            tiles.RemoveAt(height-2);
            
            //Remove a row from the grid.
            levelGrid.RowDefinitions.RemoveAt(levelGrid.RowDefinitions.Count()-1);
        }

        private void horizontalPlus_Click(object sender, RoutedEventArgs e)
        {
            if (levelGrid.ColumnDefinitions.Count() >= maxGridWidth)
            {
                return;
            }

            //Add a new column to the grid.
            ColumnDefinition GridColumn = new ColumnDefinition();
            GridColumn.Width = new GridLength(levelModel.CellSize);
            levelGrid.ColumnDefinitions.Add(GridColumn);

            //Get the new total width and height. 
            int width = levelGrid.ColumnDefinitions.Count();
            int height = levelGrid.RowDefinitions.Count();

            foreach(List<Tile> row in tiles)
            {
                row.Add(row[width-2]);
                row[width - 1].SetValue(Grid.ColumnProperty, width - 1);
            }

            //Add new tiles to the new column.
            for (int i = 0; i < height; i++)
            {
                //Walls up and down. The rest is floor.
                if (i == 0 || i == height - 1)
                {
                    Wall wall = new Wall();
                    wall.SetValue(Grid.ColumnProperty, width - 2);
                    wall.SetValue(Grid.RowProperty, i);

                    tiles[i][width-2] = wall;
                    levelGrid.Children.Add(wall);
                }
                else
                {
                    Floor floor = new Floor();
                    floor.SetValue(Grid.ColumnProperty, width -2);
                    floor.SetValue(Grid.RowProperty, i);

                    tiles[i][width - 2] = floor;
                    levelGrid.Children.Add(floor);
                }
            }
        }

        private void horizontalMin_Click(object sender, RoutedEventArgs e)
        {
            if (levelGrid.ColumnDefinitions.Count() <= minGridWidth)
            {
                return;
            }

            //Get the current total width and height. 
            int width = levelGrid.ColumnDefinitions.Count();
            int height = levelGrid.RowDefinitions.Count();

            //Move the wall column one left in both grid and array
            foreach (List<Tile> row in tiles)
            {
                levelGrid.Children.Remove(row[width - 2]);
                row[width - 1].SetValue(Grid.ColumnProperty, width - 2);
                row.RemoveAt(width - 2);
            }

            //Remove a column from the grid.
            levelGrid.ColumnDefinitions.RemoveAt(levelGrid.ColumnDefinitions.Count() - 1);
        }

        private void saveMap_Click(object sender, RoutedEventArgs e)
        {
            MapSaver save = new MapSaver(tiles, fileName.Text);
            levelReader.readAllMaps();
            mapsListBox.ItemsSource = levelModel.Maps;
        }

        private void mapsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            levelModel.StartupLevel = mapsListBox.SelectedItem.ToString();
            levelReader.readMapString();
            levelReader.readMapObject();
            loadMap();
            fileName.Text = mapsListBox.SelectedItem.ToString();
        }

        private void levelGrid_MouseDown(Object sender, MouseButtonEventArgs e)
        {
            //Er was werkelijk niks te vinden op internet betreft het vinden van een geklikte kolom in een grid. (geen werkende WPF manier tenminste)
            //Daarom zelf maar een functie geschreven die de muis coordinaten geeft binnen het levelGrid. Die delen door 40 en afronden op geheel getal geeft exact de goede kolom/rij.
            int mousePositionX = (int)Mouse.GetPosition(levelGrid).X;
            int mousePositionY = (int)Mouse.GetPosition(levelGrid).Y;
            int column = mousePositionX / levelModel.CellSize;
            int row = mousePositionY / levelModel.CellSize;
            placeTile(column, row);
        }

        private void loadMap()
        {
            //Reset the grid. 
            levelGrid.Children.Clear();
            levelGrid.ColumnDefinitions.Clear();
            levelGrid.RowDefinitions.Clear();
            tiles.Clear();

            //Create the grid columns.
            for (int i = 0; i < levelModel.ColumnLenght; i++)
            {
                ColumnDefinition GridColumn = new ColumnDefinition();
                GridColumn.Width = new GridLength(levelModel.CellSize);
                levelGrid.ColumnDefinitions.Add(GridColumn);
            }

            for (int i = 0; i < levelModel.RowLenght; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(levelModel.CellSize);
                levelGrid.RowDefinitions.Add(gridRow);
            }

            List<List<String>> map = levelModel.StringList;

            for (int y = 0; y < levelModel.RowLenght; y++)
            {
                List<Tile> row = new List<Tile>();
                for (int x = 0; x < levelModel.ColumnLenght; x++)
                {
                    Tile newTile = new Tile();
                    switch (map[y][x])
                    {
                        case "#":
                            newTile = new Wall();
                            break;
                        case "x":
                            newTile = new Target();
                            break;
                        case "o":
                            newTile = new Box();
                            break;
                        case "@":
                            newTile = new Forklift();
                            break;
                        default:
                            newTile = new Floor();
                            break;
                    }

                    newTile.SetValue(Grid.ColumnProperty, x);
                    newTile.SetValue(Grid.RowProperty, y);

                    row.Add(newTile);
                    levelGrid.Children.Add(newTile);
                }
                tiles.Add(row);
            }
        }

        private void placeTile(int x, int y)
        {
            int width = levelGrid.ColumnDefinitions.Count();
            int height = levelGrid.RowDefinitions.Count();

            if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
            {
                return;
            }

            Tile newTile;
            switch (tilesListBox.SelectedItem.ToString())
            {
                case "Sokoban.Wall":
                    newTile = new Wall();
                    break;
                case "Sokoban.Target":
                    newTile = new Target();
                    break;
                case "Sokoban.Box":
                    newTile = new Box();
                    break;
                case "Sokoban.Forklift":
                    newTile = new Forklift();
                    break;
                default:
                    newTile = new Floor();
                    break;
            }
            newTile.SetValue(Grid.ColumnProperty, x);
            newTile.SetValue(Grid.RowProperty, y);

            levelGrid.Children.Remove(tiles[y][x]);
            tiles[y][x] = newTile;
            levelGrid.Children.Add(tiles[y][x]);
        }

        private void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            String file;
            file = "map/" + fileName.Text + ".map";

            if (File.Exists(file))
            {
                File.Delete(file);
                fileName.Text = "";
                mapsListBox.SelectedIndex = 0;
                levelReader.readAllMaps();
                mapsListBox.ItemsSource = levelModel.Maps;
            }
        }
    }
}
