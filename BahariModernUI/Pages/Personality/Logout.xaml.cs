using BahariModernUI.Model;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace BahariModernUI.Pages.Personality
{
    /// <summary>
    /// Interaction logic for Logout.xaml
    /// </summary>
    public partial class Logout : UserControl
    {
        App app = (App)App.Current;
        public Logout()
        {
            InitializeComponent();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            app.LoginName = "";
            app.LoginUser = "";

            ModernDialog.ShowMessage("Logout.", "Logout", MessageBoxButton.OK);

            BBCodeBlock bs = new BBCodeBlock();
            try
            {
                bs.LinkNavigator.Navigate(new Uri("/Pages/Personality/Login.xaml", UriKind.Relative), this);

            }
            catch (Exception error)
            {
                ModernDialog.ShowMessage(error.Message, FirstFloor.ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
            }

        }

        private void Reload(object sender, RoutedEventArgs e)
        {
            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                DataTable users;
                String query = "SELECT NAME \"Name\" ";
                query += "FROM USER ";
                query += "WHERE USERNAME = '" + app.LoginUser + "';";

                users = db.GetDataTable(query);

                // Or looped through for some other reason
                foreach (DataRow r in users.Rows)
                {
                    this.name.Text = r["Name"].ToString().ToUpper();
                    //this.score.Text = "0";
                    //this.photo.Source = null;
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
