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

namespace BahariModernUI.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : UserControl
    {
        public GamePage()
        {
            InitializeComponent();

            webBrowser.Navigate("C:/Users/user/Documents/Visual Studio 2013/Projects/BahariModernUI/BahariModernUI/Pages/Game/FishyFlashGame.htm");
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            webBrowser.Navigate("C:/Users/user/Documents/Visual Studio 2013/Projects/BahariModernUI/BahariModernUI/Pages/Game/FishyFlashGame.htm");
        }
    }
}
