using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Sokoban
{
    class InfoGrid : Grid
    {
        private ModelLevel levelModel;
        private ModelScore scoreModel;
        private HighScore highScore;

        private DispatcherTimer clock;

        private Label lTimer, lMoves, lTimerCont, lMovesCont, lTarget, lTargetCont, lTargetContAmount;
        private Button bReturn;
        private Grid topTenGrid;

        private int iFontSize = 14;

        public InfoGrid(ModelLevel ml, ModelScore t, HighScore hs)
        {
            levelModel = ml;
            scoreModel = t;
            highScore = hs;

            this.Width = levelModel.InfoGridWidth;

            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFrom("#FF5EC5F5");

            createGrid();
            createTimer();

            clock = new System.Windows.Threading.DispatcherTimer();
            clock.Tick += new EventHandler(clock_Tick);
            clock.Interval = new TimeSpan(0, 0, 1);
            clock.Start();

            initAll();
        }

        private void createGrid()
        {
            RowDefinition r = new RowDefinition();
            r.Height = new GridLength(40, GridUnitType.Pixel);

            RowDefinition r1 = new RowDefinition();
            r1.Height = new GridLength(40, GridUnitType.Pixel);

            RowDefinition r2 = new RowDefinition();
            r2.Height = new GridLength(40, GridUnitType.Pixel);

            RowDefinition r3 = new RowDefinition();
            r3.Height = new GridLength(40, GridUnitType.Pixel);

            RowDefinition r4 = new RowDefinition();
            r4.Height = new GridLength(1, GridUnitType.Star);

            this.RowDefinitions.Add(r);
            this.RowDefinitions.Add(r1);
            this.RowDefinitions.Add(r2);
            this.RowDefinitions.Add(r3);
            this.RowDefinitions.Add(r4);
        }

        //Create and starts the timer (seconds)
        private void createTimer()
        {
            clock = new System.Windows.Threading.DispatcherTimer();
            clock.Tick += new EventHandler(clock_Tick);
            clock.Interval = new TimeSpan(0, 0, 1, 1);
            clock.Start();
        }

        private void initAll()
        {
            //Button
            bReturn = new Button();
            bReturn.Content = "Geef het op...";
            bReturn.SetValue(Grid.ColumnProperty, 0);
            bReturn.SetValue(Grid.RowProperty, 0);

            this.Children.Add(bReturn);

            //Labels for timer
            lTimer = new Label();
            lTimer.Content = "Speeltijd:  ";
            lTimer.FontSize = iFontSize;
            lTimer.Margin = new Thickness(0, 8, 0, 0);
            lTimer.SetValue(Grid.ColumnProperty, 0);
            lTimer.SetValue(Grid.RowProperty, 1);

            this.Children.Add(lTimer);

            lTimerCont = new Label();
            lTimerCont.Margin = new Thickness(120, 8, 0, 0);
            lTimerCont.FontSize = iFontSize;
            lTimerCont.Content = 0;
            lTimerCont.SetValue(Grid.ColumnProperty, 0);
            lTimerCont.SetValue(Grid.RowProperty, 1);

            this.Children.Add(lTimerCont);

            //Labels for moves
            lMoves = new Label();
            lMoves.Content = "Aantal stappen:  ";
            lMoves.FontSize = iFontSize;
            lMoves.Margin = new Thickness(0, 8, 0, 0);
            lMoves.SetValue(Grid.ColumnProperty, 0);
            lMoves.SetValue(Grid.RowProperty, 2);

            this.Children.Add(lMoves);

            lMovesCont = new Label();
            lMovesCont.Margin = new Thickness(120, 8, 0, 0);
            lMovesCont.FontSize = iFontSize;
            lMovesCont.Content = 0;
            lMovesCont.SetValue(Grid.ColumnProperty, 0);
            lMovesCont.SetValue(Grid.RowProperty, 2);

            this.Children.Add(lMovesCont);

            //Labels for targets
            lTarget = new Label();
            lTarget.Content = "Targets:  ";
            lTarget.FontSize = iFontSize;
            lTarget.Margin = new Thickness(0, 8, 0, 0);
            lTarget.SetValue(Grid.ColumnProperty, 0);
            lTarget.SetValue(Grid.RowProperty, 3);

            this.Children.Add(lTarget);

            lTargetCont = new Label();
            lTargetCont.Margin = new Thickness(120, 8, 0, 0);
            lTargetCont.FontSize = iFontSize;
            lTargetCont.Content = levelModel.AmountOfTargetsDone;
            lTargetCont.SetValue(Grid.ColumnProperty, 0);
            lTargetCont.SetValue(Grid.RowProperty, 3);

            this.Children.Add(lTargetCont);

            lTargetContAmount = new Label();
            lTargetContAmount.Margin = new Thickness(130, 8, 0, 0);
            lTargetContAmount.FontSize = iFontSize;
            lTargetContAmount.Content = "/ " + levelModel.AmountOfTargets;
            lTargetContAmount.SetValue(Grid.ColumnProperty, 0);
            lTargetContAmount.SetValue(Grid.RowProperty, 3);

            this.Children.Add(lTargetContAmount);

            showTopTen();
        }

        private void showTopTen()
        {
            ModelScore[] topTen = highScore.getHighScore(levelModel.StartupLevel, 10).ToArray();
            topTenGrid = new Grid();

            int headerHeight = 30, rowHeight = 25;
            
            //Position and look of the Grid
            topTenGrid.Margin = new Thickness(5, 0, 5, 5);
            topTenGrid.SetValue(Grid.ColumnProperty, 0);
            topTenGrid.SetValue(Grid.RowProperty, 4);
            topTenGrid.ShowGridLines = true;
            topTenGrid.MaxHeight = headerHeight + (rowHeight*topTen.Length);
            topTenGrid.VerticalAlignment = VerticalAlignment.Top;

            var bc = new BrushConverter();
            topTenGrid.Background = (Brush)bc.ConvertFrom("#E3E3E3");

            //Create the columns
            ColumnDefinition playerColumn = new ColumnDefinition();
            playerColumn.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinition movesColumn = new ColumnDefinition();
            movesColumn.Width = new GridLength();
            ColumnDefinition timeColumn = new ColumnDefinition();
            timeColumn.Width = new GridLength();

            //Add the columns
            topTenGrid.ColumnDefinitions.Add(playerColumn);
            topTenGrid.ColumnDefinitions.Add(movesColumn);
            topTenGrid.ColumnDefinitions.Add(timeColumn);

            //add the first row and create the header labels. 
            RowDefinition header = new RowDefinition();
            header.Height = new GridLength(headerHeight, GridUnitType.Pixel);
            topTenGrid.RowDefinitions.Add(header);
            
            Label playerHeader = new Label();
            playerHeader.Content = "Player";
            playerHeader.SetValue(Grid.ColumnProperty, 0);
            playerHeader.SetValue(Grid.RowProperty, 0);
            playerHeader.HorizontalAlignment = HorizontalAlignment.Center;
            playerHeader.VerticalAlignment = VerticalAlignment.Center;
            playerHeader.FontSize = iFontSize;
            playerHeader.FontWeight = FontWeights.Bold;

            Label movesHeader = new Label();
            movesHeader.Content = "Moves";
            movesHeader.SetValue(Grid.ColumnProperty, 1);
            movesHeader.SetValue(Grid.RowProperty, 0);
            movesHeader.HorizontalAlignment = HorizontalAlignment.Center;
            movesHeader.VerticalAlignment = VerticalAlignment.Center;
            movesHeader.FontSize = iFontSize;
            movesHeader.FontWeight = FontWeights.Bold;

            Label timeHeader = new Label();
            timeHeader.Content = "Time";
            timeHeader.SetValue(Grid.ColumnProperty, 2);
            timeHeader.SetValue(Grid.RowProperty, 0);
            timeHeader.HorizontalAlignment = HorizontalAlignment.Center;
            timeHeader.VerticalAlignment = VerticalAlignment.Center;
            timeHeader.FontSize = iFontSize;
            timeHeader.FontWeight = FontWeights.Bold;

            topTenGrid.Children.Add(playerHeader);
            topTenGrid.Children.Add(movesHeader);
            topTenGrid.Children.Add(timeHeader);

            int rowNumber = 1;
            foreach(ModelScore score in topTen)
            {
                //Create the row
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(rowHeight, GridUnitType.Pixel);
                topTenGrid.RowDefinitions.Add(row);

                //Add the information to the row columns.. 
                Label player = new Label();
                player.Content = score.PlayerName;
                player.SetValue(Grid.ColumnProperty, 0);
                player.SetValue(Grid.RowProperty, rowNumber);
                topTenGrid.Children.Add(player);

                Label moves = new Label();
                moves.Content = score.Moves;
                moves.SetValue(Grid.ColumnProperty, 1);
                moves.SetValue(Grid.RowProperty, rowNumber);
                topTenGrid.Children.Add(moves);

                Label time = new Label();
                time.Content = (score.Time / 60) +":"+ (score.Time % 60);
                time.SetValue(Grid.ColumnProperty, 2);
                time.SetValue(Grid.RowProperty, rowNumber);
                topTenGrid.Children.Add(time);
                
                rowNumber++;
            }

            this.Children.Add(topTenGrid);
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            scoreModel.Time++;

            lTimerCont.Content = scoreModel.Time;

            lMovesCont.Content = scoreModel.Moves;

            lTargetCont.Content = levelModel.AmountOfTargetsDone;
        }
    }
}
