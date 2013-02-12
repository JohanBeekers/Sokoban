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
        public InputBox()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;
            question.Content = "You won, great leader!\r\nGive your name for in the highscore list.";
        }

        private void Ok_Button(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        public String show()
        {
            this.Visibility = Visibility.Visible;
            return userName.Text;
        }

    }
}
