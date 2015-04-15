using BahariModernUI.Model;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
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

namespace BahariModernUI.Pages.Settings
{
    /// <summary>
    /// Interaction logic for Appearance.xaml
    /// </summary>
    public partial class Appearance : UserControl
    {
        public Appearance()
        {
            InitializeComponent();

            // create and assign the appearance view model
            this.DataContext = new AppearanceViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SQLiteDatabase db = new SQLiteDatabase();

            Dictionary<String, String> data = new Dictionary<String, String>();

            var a = theme.SelectedItem as Link;

            data.Add("THEME", a.DisplayName.ToString());
            data.Add("COLOR", brush.SelectedItem.ToString());
            data.Add("FONT", font.SelectedItem.ToString());

            //ModernDialog.ShowMessage(data.ElementAt(0).Value + " " + data.ElementAt(1).Value + " " + data.ElementAt(2).Value, "Themes", MessageBoxButton.OK);

            string condition = "ID = 1;";
            try
            {
                if (db.Update("SETTING", data, condition) == true)
                {
                    ModernDialog.ShowMessage("Saved :) .", "Themes", MessageBoxButton.OK);
                }
                else
                {
                    ModernDialog.ShowMessage("Failed :( , Please try again.", "Themes", MessageBoxButton.OK);
                }
            }
            catch (Exception crap)
            {
                MessageBox.Show(crap.Message);
            }
        }
    }
}
