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

namespace TestApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int levelGridCellSize = 40;
        Grid levelGrid;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void createGrid(int x, int y)
        {
            Border border = new Border();
            border.Background = new SolidColorBrush(Colors.Black);
            levelGrid = new Grid();
            level.Children.Add(levelGrid);
            levelGrid.Background = new SolidColorBrush(Colors.Green);
            levelGrid.Width = x * levelGridCellSize;
            levelGrid.Height = y * levelGridCellSize;
            levelGrid.ShowGridLines = true;
            levelGrid.MouseDown += new MouseButtonEventHandler(levelGrid_MouseDown);

            for (int i = 0; i < x; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(levelGridCellSize);
                levelGrid.ColumnDefinitions.Add(col);
            }

            for (int i = 0; i < y; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(levelGridCellSize);
                levelGrid.RowDefinitions.Add(row);

                for (int j = 0; j < y; j++)
                {
                    Label label = new Label();
                    label.Content = i + " " + j;
                    label.SetValue(Grid.ColumnProperty, j);
                    label.SetValue(Grid.RowProperty, i);
                    levelGrid.Children.Add(label);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            level.Children.Clear();
            createGrid(Convert.ToInt32(textBoxX.Text), Convert.ToInt32(textboxY.Text));
        }

        private void levelGrid_MouseDown(Object sender, MouseButtonEventArgs e)
        {
            //Er was werkelijk niks te vinden op internet betreft het vinden van een geklikte kolom in een grid. (geen werkende WPF manier tenminste)
            //Daarom zelf maar een functie geschreven die de muis coordinaten geeft binnen het levelGrid. Die delen door 40 en afronden op geheel getal geeft exact de goede kolom/rij.
            int mousePositionX = (int)Mouse.GetPosition(levelGrid).X;
            int mousePositionY = (int)Mouse.GetPosition(levelGrid).Y;
            int column = mousePositionX / 40;
            int row = mousePositionY / 40;
            MessageBox.Show(row + " " + column);
        }
    }
}
