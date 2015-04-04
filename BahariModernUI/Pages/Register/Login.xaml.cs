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

namespace BahariModernUI.Pages.Register
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.user.Text == "" || this.password.Password == "")
            {
                ModernDialog.ShowMessage("Please fill the blank username or password.", "Missing your name or password", MessageBoxButton.OK);

            }
            else
            {
                try
                {
                    SQLiteDatabase db = new SQLiteDatabase();
                    DataTable users;
                    String query = "SELECT NAME \"Name\", PASSWORD \"Password\" ";
                    query += "FROM USER ";
                    query += "WHERE USERNAME = '" + this.user.Text.ToString() + "';";

                    //ModernDialog.ShowMessage(query, "", MessageBoxButton.OK);

                    users = db.GetDataTable(query);

                    String password = "";
                    String name = "";
                    
                    // Or looped through for some other reason
                    foreach (DataRow r in users.Rows)
                    {
                        password = r["Password"].ToString();
                        //ModernDialog.ShowMessage(password, "", MessageBoxButton.OK);
                        name = r["Name"].ToString();
                    }
	                
                    if (password == this.password.Password)
                    {
                        App app = (App)App.Current;
                        app.LoginName = name;
                        app.LoginUser = this.user.Text;
                        ModernDialog.ShowMessage("Login.", "Login", MessageBoxButton.OK);
                        this.user.Text = "";
                        this.password.Password = "";
                        //BBCodeBlock bs = new BBCodeBlock();
                        //try
                        //{
                        //    bs.LinkNavigator.Navigate(new Uri("/Pages/Home.xaml", UriKind.Relative), this);
                            
                        //}
                        //catch (Exception error)
                        //{
                        //    ModernDialog.ShowMessage(error.Message, FirstFloor.ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
                        //}
                        
                    }
                    else
                    {
                        ModernDialog.ShowMessage("Not Login.", "Login", MessageBoxButton.OK);
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
}
