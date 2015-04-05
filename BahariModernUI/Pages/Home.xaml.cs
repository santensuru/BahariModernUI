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
            Pushpin center = new Pushpin();
            center.Location = new Location(-5, 120);

            ToolTipService.SetToolTip(center, "Indonesia yey :D");
            myMap.Children.Add(center);
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
