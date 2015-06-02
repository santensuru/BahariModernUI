using BahariModernUI.Model;
using BahariModernUI.Pages.Appendix;
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
        
        public Maps()
        {
            InitializeComponent();

            fresh.ToolTip = BahariModernUI.Resources.StringResources.Fresh;
            sea.ToolTip = BahariModernUI.Resources.StringResources.Sea;
            //sea.IsEnabled = false;
            reef.ToolTip = BahariModernUI.Resources.StringResources.Reef;
            conservation.ToolTip = BahariModernUI.Resources.StringResources.Conservation;

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

            //fresh.IsEnabled = false;
            //sea.IsEnabled = true;
            //reef.IsEnabled = true;
            //conservation.IsEnabled = true;

            Reload();
        }

        private void Sea_Button(object sender, RoutedEventArgs e)
        {
            selected = "Biota Laut";

            //fresh.IsEnabled = true;
            //sea.IsEnabled = false;
            //reef.IsEnabled = true;
            //conservation.IsEnabled = true;

            Reload();
        }

        private void Reef_Button(object sender, RoutedEventArgs e)
        {
            selected = "Terumbu Karang";

            //fresh.IsEnabled = true;
            //sea.IsEnabled = true;
            //reef.IsEnabled = false;
            //conservation.IsEnabled = true;

            Reload();
        }

        private void Conservation_Button(object sender, RoutedEventArgs e)
        {
            selected = "Kawasan Konservasi";

            //fresh.IsEnabled = true;
            //sea.IsEnabled = true;
            //reef.IsEnabled = true;
            //conservation.IsEnabled = false;

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
    }
}
