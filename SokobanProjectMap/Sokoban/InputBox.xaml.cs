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
using System.Windows.Threading;

namespace Sokoban
{
    /// <summary>
    /// Interaction logic for inputBox.xaml
    /// </summary>
    public partial class InputBox : UserControl
    {
        MainWindow mainWindow;

        public InputBox(MainWindow mainWindow)
        {
            InitializeComponent();
            question.Content = "You won!\r\nGive your name for in the highscore list.";
            this.mainWindow = mainWindow;
        }

        private void Ok_Button(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            mainWindow.saveScore(userName.Text);
            mainWindow.nextMap();
        }

    }
}
