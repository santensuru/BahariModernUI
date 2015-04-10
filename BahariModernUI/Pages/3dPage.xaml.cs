﻿using FirstFloor.ModernUI.Windows.Controls;
using HelixToolkit.Wpf;
using HelixToolkit.Wpf.SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MapPage.xaml
    /// </summary>
    public partial class MapPage : UserControl
    {
        public MapPage()
        {
            InitializeComponent();
            

            //background.AlignmentX = AlignmentX.Right;

            //var mi = new ModelImporter();
            //System.Windows.Media.Media3D.Model3DGroup currentModel = mi.Load("C:\\Users\\user\\Downloads\\Compressed\\1\\Amago.max", null, true);

            //// Display the model
            //foo.Content = currentModel;

            HelixToolkit.Wpf.ObjReader CurrentHelixObjReader = new HelixToolkit.Wpf.ObjReader();

            Uri uri = new Uri("C:/Users/user/Downloads/Compressed/jelly fish series jelly fish one/jellyfish series jelly fish one improved.obj", UriKind.Relative);
            //ModernDialog.ShowMessage(uri.ToString(), "", MessageBoxButton.OK);

            System.Windows.Media.Media3D.Model3DGroup MyModel = CurrentHelixObjReader.Read(uri.ToString());

            // Display the model
            foo.Content = MyModel;

            //foo.Transform.

        }

        private void myView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //text.Text = myView.CurrentPosition.ToString();
        }

        private void txtUserEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ModernDialog.ShowMessage(txtUserEntry.Text.ToString(), "", MessageBoxButton.OK);


            }
        }

    }
}
