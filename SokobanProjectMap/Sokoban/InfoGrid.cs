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

        private DispatcherTimer clock;

        private Label lTimer, lMoves, lTimerCont, lMovesCont, lTarget, lTargetCont, lTargetContAmount;
        private Button bReturn;

        private int iFontSize = 14;

        public InfoGrid(ModelLevel ml, ModelScore t)
        {
            levelModel = ml;
            scoreModel = t;

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


            this.RowDefinitions.Add(r);
            this.RowDefinitions.Add(r1);
            this.RowDefinitions.Add(r2);
            this.RowDefinitions.Add(r3);
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
            lTargetContAmount.Content = "/" + levelModel.AmountOfTargets;
            lTargetContAmount.SetValue(Grid.ColumnProperty, 0);
            lTargetContAmount.SetValue(Grid.RowProperty, 3);

            this.Children.Add(lTargetContAmount);
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
