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
    /// Interaction logic for LevelEditor.xaml
    /// </summary>
    public partial class LevelEditor : UserControl
    {
        ModelLevel levelModel;

        public LevelEditor(ModelLevel levelModel)
        {
            InitializeComponent();
            this.levelModel = levelModel;

            Binding mapsListBinding = new Binding("maps");
            mapsListBinding.Source = levelModel;
            mapsListBox.SetBinding(ListBox.ItemsSourceProperty, mapsListBinding);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
