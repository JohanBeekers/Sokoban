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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModelLevel levelModel;
        private LevelReader levelReader;
        private Player player;
        private Board board;
        private InfoGrid infoGrid;
        private ModelScore scoreModel;

        public MainWindow()
        {
            InitializeComponent();

            levelModel = new ModelLevel();
            levelReader = new LevelReader(levelModel);

            fillDropdown();
        }

        //Fille the dropdown menu from the array of maps in levelReader
        private void fillDropdown()
        {
            //Loop through the array Maps of levelReader
            for (int i = 0; i < levelModel.Maps.Count(); i++)
            {
                //Add each item in the array to the combobox comboMaps
                comboMaps.Items.Add(levelModel.Maps[i]);
            }
        }

        //Method if highscore clicked
        private void highscore_clicked(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Highscore");
        }

        //Method if mouse hover highscore
        private void highscore_mouseEnter(object sender, MouseEventArgs e)
        {
            labelHighscore.Foreground = Brushes.Black;
        }

        //Method if mouse leave highscore
        private void highscore_mouseLeave(object sender, MouseEventArgs e)
        {
            labelHighscore.Foreground = Brushes.White;
        }

        //Method if start clicked
        private void start_clicked(object sender, MouseButtonEventArgs e)
        {
            startLevel(comboMaps.SelectedItem.ToString());
        }

        //Method if mouse hover start
        private void start_mouseEnter(object sender, MouseEventArgs e)
        {
            labelStart.Foreground = Brushes.Black;
        }

        //Method if mouse leave start
        private void start_mouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            labelStart.Foreground = (Brush)bc.ConvertFrom("#FF5EC5F5");
        }

        public void startLevel(string level)
        {
            levelModel.StartupLevel = level;

            levelReader.readMapString();
            levelReader.readMapObject();

            //Change the width and height and the title of the window
            this.Width = 16 + (levelModel.ColumnLenght * levelModel.CellSize) + levelModel.InfoGridWidth;
            this.Height = 39 + (levelModel.RowLenght * levelModel.CellSize);
            this.MinHeight = 39 + (8 * levelModel.CellSize);
            this.Title += ": " + levelModel.StartupLevel;

            colInfoGrid.Width = new GridLength(levelModel.InfoGridWidth);

            board = new Board(this.levelModel);
            board.SetValue(Grid.ColumnProperty, 0);
            board.SetValue(Grid.RowProperty, 0);
            gameGrid.Children.Add(board);

            scoreModel = new ModelScore();

            infoGrid = new InfoGrid(this.levelModel, scoreModel);
            infoGrid.SetValue(Grid.ColumnProperty, 1);
            infoGrid.SetValue(Grid.RowProperty, 0);
            gameGrid.Children.Add(infoGrid);

            player = new Player(board, levelModel, scoreModel, this);

            mainGrid.Visibility = Visibility.Collapsed;
            gameGrid.Visibility = Visibility.Visible;
        }

        private void key_Down(object sender, KeyEventArgs e)
        {
            if (levelModel.LevelStarted == true)
            {
                player.keyPressed(e);
            }
        }

        public void nextMap()
        {
            for (int i = 0; i < levelModel.Maps.Count(); i++)
                        {
                            if (levelModel.Maps[i].ToString().Equals(levelModel.StartupLevel))
                            {
                                if (i + 1 >= levelModel.Maps.Count())
                                {
                                    MessageBox.Show("laatste map");
                                }
                                else
                                {
                                    MessageBox.Show("You won, great leader");
                                    /**
                                    player = null;
                                    board = null;
                                    infoGrid = null;
                                    scoreModel = null;
                                    mainGrid = null;

                                    string nextmap = levelModel.Maps[i + 1];
                                    //this.startLevel(nextmap);
                                     */
                                }
                            }
                        }
        }
    }
}
