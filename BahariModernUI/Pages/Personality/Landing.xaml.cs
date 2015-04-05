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
    /// Interaction logic for Landing.xaml
    /// </summary>
    public partial class Landing : UserControl
    {
        App app = (App) App.Current;
        public Landing()
        {
            InitializeComponent();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            //if (app.LoginName != "")
            //{
            //    BBCodeBlock bs = new BBCodeBlock();
            //    try
            //    {
            //        bs.LinkNavigator.Navigate(new Uri("/Pages/Personality/Logout.xaml", UriKind.Relative), this);
                    

            //    }
            //    catch (Exception error)
            //    {
            //        ModernDialog.ShowMessage(error.Message, FirstFloor.ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
            //    }
            //}
            //else
            //{
            //    BBCodeBlock bs = new BBCodeBlock();
            //    try
            //    {
            //        bs.LinkNavigator.Navigate(new Uri("/Pages/Personality/Login.xaml", UriKind.Relative), this);

            //    }
            //    catch (Exception error)
            //    {
            //        ModernDialog.ShowMessage(error.Message, FirstFloor.ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
            //    }
            //}

            if (app.LoginName != "")
            {
                this.login.Visibility = System.Windows.Visibility.Hidden;
                this.logout.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.login.Visibility = System.Windows.Visibility.Visible; 
                this.logout.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
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
                        app.LoginName = name;
                        app.LoginUser = this.user.Text;
                        ModernDialog.ShowMessage("Login.", "Login", MessageBoxButton.OK);
                        this.user.Text = "";
                        this.password.Password = "";
                        this.login.Visibility = System.Windows.Visibility.Hidden;
                        this.logout.Visibility = System.Windows.Visibility.Visible;
                        Reload();

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

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (this.regname.Text == "" || this.reguser.Text == "" || this.regpassword.Password == "")
            {
                ModernDialog.ShowMessage("Please fill the blank name, username or password.", "Missing your name or password", MessageBoxButton.OK);

            }
            else
            {
                SQLiteDatabase db = new SQLiteDatabase();
                Dictionary<String, String> data = new Dictionary<String, String>();
                data.Add("NAME", this.regname.Text);
                data.Add("USERNAME", this.reguser.Text);
                data.Add("PASSWORD", this.regpassword.Password);
                try
                {
                    if (db.Insert("USER", data) == true)
                    {
                        app.LoginName = this.regname.Text;
                        app.LoginUser = this.reguser.Text;
                        ModernDialog.ShowMessage("Success :) . Auto login.", "Registration", MessageBoxButton.OK);
                        this.login.Visibility = System.Windows.Visibility.Hidden;
                        this.logout.Visibility = System.Windows.Visibility.Visible;
                        Reload();
                    }
                    else
                    {
                        ModernDialog.ShowMessage("Failed :( , Please try again.", "Registration", MessageBoxButton.OK);
                    }
                }
                catch (Exception crap)
                {
                    MessageBox.Show(crap.Message);
                }
                this.regname.Text = "";
                this.reguser.Text = "";
                this.regpassword.Password = "";
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            app.LoginName = "";
            app.LoginUser = "";

            ModernDialog.ShowMessage("Logout.", "Logout", MessageBoxButton.OK);

            this.login.Visibility = System.Windows.Visibility.Visible;
            this.logout.Visibility = System.Windows.Visibility.Hidden;

        }

        private void Reload()
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
