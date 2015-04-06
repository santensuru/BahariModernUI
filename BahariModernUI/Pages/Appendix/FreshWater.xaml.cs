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

namespace BahariModernUI.Pages.Appendix
{
    /// <summary>
    /// Interaction logic for FreshWater.xaml
    /// </summary>
    public partial class FreshWater : UserControl
    {
        public FreshWater()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            var _mainWindow = (MainWindow)Application.Current.MainWindow;

            Detail newdialoge = new Detail("Param");
            newdialoge.Owner = _mainWindow;
            newdialoge.ShowDialog();
        }
    }
}
