using BahariModernUI.Pages.Appendix;
using Microsoft.Maps.MapControl.WPF;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            Pushpin[] center = new Pushpin[2];
            center[0] = new Pushpin();
            center[0].Location = new Location(-5, 120);

            ToolTipService.SetToolTip(center[0], "Indonesia yey :D");
            center[0].MouseLeftButtonDown += new MouseButtonEventHandler(Left_Click);
            myMap.Children.Add(center[0]);

            center[1] = new Pushpin();
            center[1].Location = new Location(4, 109);

            ToolTipService.SetToolTip(center[1], "Coba 2");
            center[1].MouseLeftButtonDown += new MouseButtonEventHandler(Left_Click);
            myMap.Children.Add(center[1]);
        }

        private void Left_Click(object sender, MouseButtonEventArgs e)
        {
            var _mainWindow = (MainWindow)Application.Current.MainWindow;

            Detail newdialoge = new Detail("Param");
            newdialoge.Owner = _mainWindow;
            newdialoge.ShowDialog();
        }

        //private void Get_Button(object sender, RoutedEventArgs e)
        //{
            
        //}

        //private void Plus_Button(object sender, RoutedEventArgs e)
        //{

        //}

        //private void Minus_Button(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
