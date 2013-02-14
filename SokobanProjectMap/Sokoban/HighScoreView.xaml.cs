using System;
using System.Collections.Generic;
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
    /// Interaction logic for HighScoreView.xaml
    /// </summary>
    public partial class HighScoreView : UserControl
    {
        HighScore highScore;
        private static int rowHeight = 25;

        public HighScoreView(ModelLevel level, HighScore highScore)
        {
            InitializeComponent();

            levelList.ItemsSource = level.Maps;
            this.highScore = highScore;
            levelList.SelectedIndex = 0;
            showScore(levelList.SelectedItem.ToString());
        }

        private void levelList_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            showScore(levelList.SelectedItem.ToString());
        }

        private void showScore(String map)
        {
            //Remove all rows except for the first one. (The header row)
            highScoreGrid.RowDefinitions.Clear();
            highScoreGrid.Children.Clear();
            highScoreGrid.RowDefinitions.Add(new RowDefinition());
            highScoreGrid.Children.Add(headerPlayer);
            highScoreGrid.Children.Add(headerMoves);
            highScoreGrid.Children.Add(headerTimer);

            //Get the highscores for the chosen map.
            ModelScore[] scores = highScore.getHighScore(map).ToArray();

            //Show the scores.
            int rowNumber = 1;
            foreach (ModelScore score in scores)
            {
                //Create the row
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(rowHeight, GridUnitType.Pixel);
                highScoreGrid.RowDefinitions.Add(row);

                //Add the information to the row columns.. 
                Label player = new Label();
                player.Content = score.PlayerName;
                player.SetValue(Grid.ColumnProperty, 0);
                player.SetValue(Grid.RowProperty, rowNumber);
                highScoreGrid.Children.Add(player);

                Label moves = new Label();
                moves.Content = score.Moves;
                moves.SetValue(Grid.ColumnProperty, 1);
                moves.SetValue(Grid.RowProperty, rowNumber);
                highScoreGrid.Children.Add(moves);

                Label time = new Label();
                time.Content = (score.Time / 60) + ":" + (score.Time % 60);
                time.SetValue(Grid.ColumnProperty, 2);
                time.SetValue(Grid.RowProperty, rowNumber);
                highScoreGrid.Children.Add(time);

                rowNumber++;
            }
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
