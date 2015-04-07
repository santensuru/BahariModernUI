﻿using BahariModernUI.Pages.Appendix;
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
        Pushpin[] center = new Pushpin[3];
        
        public Home()
        {
            InitializeComponent();
            //Pushpin[] center = new Pushpin[2];
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

            center[2] = new Pushpin();
            center[2].Location = new Location(3, 109);

            ToolTipService.SetToolTip(center[2], "Coba 3");
            center[2].MouseLeftButtonDown += new MouseButtonEventHandler(Left_Click);
            myMap.Children.Add(center[2]);
        }

        private void Left_Click(object sender, MouseButtonEventArgs e)
        {
            

            Point mousePosition = e.GetPosition(this);
            Location pinLocation = myMap.ViewportPointToLocation(mousePosition);
            var lat = pinLocation.Latitude;
            var lon = pinLocation.Longitude;
            //lat.ToString() + " " + lon.ToString()

            double[] location = new double[3];
            int i;
            double min = 999999999;
            int posision = 0;
            for (i = 0; i < 3; i++)
            {
                location[i] = CekDistance(center[i].Location, pinLocation);
                if (location[i] < min)
                {
                    min = location[i];
                    posision = i;
                }
            }
            //double a = CekDistance(center[0].Location, pinLocation);
            //double b = CekDistance(center[1].Location, pinLocation);

            //string text;

            //if (a < b)
            //{
            //    text = ToolTipService.GetToolTip(center[0]).ToString();
            //}
            //else
            //{
            //    text = ToolTipService.GetToolTip(center[1]).ToString();
            //}

            string text = ToolTipService.GetToolTip(center[posision]).ToString();

            var _mainWindow = (MainWindow)Application.Current.MainWindow;

            Detail newdialoge = new Detail(text);
            newdialoge.Owner = _mainWindow;
            newdialoge.ShowDialog();

            e.Handled = true;

        }

        private double CekDistance(Location a, Location b)
        {
            //return Math.Sqrt(Math.Pow(a.Longitude - b.Longitude, 2) + Math.Pow(a.Latitude - b.Latitude, 2));
            return Math.Abs(a.Longitude - b.Longitude) + Math.Abs(a.Latitude - b.Latitude);
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
