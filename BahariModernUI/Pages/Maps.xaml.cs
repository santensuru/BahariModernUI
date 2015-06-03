using BahariModernUI.Model;
using BahariModernUI.Pages.Appendix;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for Maps.xaml
    /// </summary>
    public partial class Maps : UserControl
    {
        Pushpin[] center;
        static int tot;

        String selected = "Biota Laut";
        byte[] active = new byte[4];

        static Uri baseUri;
        
        public Maps()
        {
            InitializeComponent();

            baseUri = BaseUriHelper.GetBaseUri(this);

            Load(this, null);

            fresh.ToolTip = BahariModernUI.Resources.StringResources.Fresh;
            sea.ToolTip = BahariModernUI.Resources.StringResources.Sea;
            sea.IsEnabled = false;
            //sea.Opacity = 0.75;
            sea.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));
            reef.ToolTip = BahariModernUI.Resources.StringResources.Reef;
            conservation.ToolTip = BahariModernUI.Resources.StringResources.Conservation;
            //ModernDialog.ShowMessage(Colors.Aquamarine.ToString(), "", MessageBoxButton.OK);

            state.Content = BahariModernUI.Resources.StringResources.Sea;

            Reload();
        }

        private void Left_Click(object sender, MouseButtonEventArgs e)
        {
            Point mousePosition = e.GetPosition(this);
            Location pinLocation = myMap.ViewportPointToLocation(mousePosition);
            var lat = pinLocation.Latitude;
            var lon = pinLocation.Longitude;

            double[] location = new double[3];
            int i;
            double min = 999999999;
            int posision = 0;
            for (i = 0; i < tot; i++)
            {
                location[i] = CekDistance(center[i].Location, pinLocation);
                if (location[i] < min)
                {
                    min = location[i];
                    posision = i;
                }
            }

            string text = ToolTipService.GetToolTip(center[posision]).ToString();

            var _mainWindow = (MainWindow)Application.Current.MainWindow;

            if (selected != "Kawasan Konservasi")
            {
                Detail newdialoge = new Detail(text);
                newdialoge.Owner = _mainWindow;
                newdialoge.ShowDialog();
            }
            else
            {
                DetailKawasan newdialoge = new DetailKawasan(text);
                newdialoge.Owner = _mainWindow;
                newdialoge.ShowDialog();
            }
            e.Handled = true;
        }

        private double CekDistance(Location a, Location b)
        {
            return Math.Abs(a.Longitude - b.Longitude) + Math.Abs(a.Latitude - b.Latitude);
        }

        private void Fresh_Button(object sender, RoutedEventArgs e)
        {
            selected = "Biota Air Tawar";
            state.Content = BahariModernUI.Resources.StringResources.Fresh;

            fresh.IsEnabled = false;
            //fresh.Opacity = 0.75;
            fresh.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));
            sea.IsEnabled = true;
            //sea.Opacity = 1;
            reef.IsEnabled = true;
            //reef.Opacity = 1;
            conservation.IsEnabled = true;
            //conservation.Opacity = 1;

            sea.ClearValue(Button.BackgroundProperty);
            reef.ClearValue(Button.BackgroundProperty);
            conservation.ClearValue(Button.BackgroundProperty);

            Reload();
        }

        private void Sea_Button(object sender, RoutedEventArgs e)
        {
            selected = "Biota Laut";
            state.Content = BahariModernUI.Resources.StringResources.Sea;

            fresh.IsEnabled = true;
            //fresh.Opacity = 1;
            sea.IsEnabled = false;
            //sea.Opacity = 0.75;
            sea.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));
            reef.IsEnabled = true;
            //reef.Opacity = 1;
            conservation.IsEnabled = true;
            //conservation.Opacity = 1;

            fresh.ClearValue(Button.BackgroundProperty);
            reef.ClearValue(Button.BackgroundProperty);
            conservation.ClearValue(Button.BackgroundProperty);

            Reload();
        }

        private void Reef_Button(object sender, RoutedEventArgs e)
        {
            selected = "Terumbu Karang";
            state.Content = BahariModernUI.Resources.StringResources.Reef;

            fresh.IsEnabled = true;
            //fresh.Opacity = 1;
            sea.IsEnabled = true;
            //sea.Opacity = 1;
            reef.IsEnabled = false;
            //reef.Opacity = 0.75;
            reef.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));
            conservation.IsEnabled = true;
            //conservation.Opacity = 1;

            fresh.ClearValue(Button.BackgroundProperty);
            sea.ClearValue(Button.BackgroundProperty);
            conservation.ClearValue(Button.BackgroundProperty);

            Reload();
        }

        private void Conservation_Button(object sender, RoutedEventArgs e)
        {
            selected = "Kawasan Konservasi";
            state.Content = BahariModernUI.Resources.StringResources.Conservation;

            fresh.IsEnabled = true;
            //fresh.Opacity = 1;
            sea.IsEnabled = true;
            //sea.Opacity = 1;
            reef.IsEnabled = true;
            //reef.Opacity = 1;
            conservation.IsEnabled = false;
            //conservation.Opacity = 0.75;
            conservation.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));

            fresh.ClearValue(Button.BackgroundProperty);
            sea.ClearValue(Button.BackgroundProperty);
            reef.ClearValue(Button.BackgroundProperty);

            Reload();
        }

        private void Reload()
        {
            myMap.Children.Clear();

            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                DataTable biota;

                String query = "SELECT COUNT(*) AS TOT ";
                query += "FROM PERSEBARAN ";
                query += "WHERE JENIS LIKE '%" + selected + "%';";
                biota = db.GetDataTable(query);
                foreach (DataRow r in biota.Rows)
                {
                    tot = Convert.ToInt32(r["TOT"].ToString());
                }

                center = new Pushpin[tot];

                query = "SELECT * ";
                query += "FROM PERSEBARAN ";
                query += "WHERE JENIS LIKE '%" + selected + "%';";

                biota = db.GetDataTable(query);

                int i = 0;
                // Or looped through for some other reason
                foreach (DataRow r in biota.Rows)
                {
                    center[i] = new Pushpin();
                    center[i].Location = new Location(Convert.ToDouble(r["LATITUDE"].ToString()), Convert.ToDouble(r["LONGITUDE"].ToString()));

                    ToolTipService.SetToolTip(center[i], r["NAMA"].ToString());
                    center[i].MouseLeftButtonDown += new MouseButtonEventHandler(Left_Click);
                    myMap.Children.Add(center[i]);
                    i++;
                }
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            active = StringToByteArray(AppearanceManager.Current.AccentColor.ToString().Replace("#", ""));

            if (selected == "Biota Air Tawar")
            {
                fresh.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));
            }
            else if (selected == "Biota Laut")
            {
                sea.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));
            }
            else if (selected == "Terumbu Karang")
            {
                reef.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));
            }
            else if (selected == "Kawasan Konservasi")
            {
                conservation.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));
            }

            if (AppearanceManager.Current.ThemeSource == AppearanceManager.DarkThemeSource)
            {
                fresh1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.biota air tawar.png"));
                sea1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.biota air laut.png"));
                reef1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.terumbu karang.png"));
                conservation1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.kawasan observasi.png"));
            }
            else
            {
                fresh1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/biota air tawar.png"));
                sea1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/biota air laut.png"));
                reef1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/terumbu karang.png"));
                conservation1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/kawasan observasi.png"));
            }
        }

        static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
