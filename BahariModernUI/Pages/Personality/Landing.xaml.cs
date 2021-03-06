﻿using BahariModernUI.Model;
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

            login1.Text = BahariModernUI.Resources.StringResources.Login.ToUpper();
            login2.Content = BahariModernUI.Resources.StringResources.Login;
            register.Text = BahariModernUI.Resources.StringResources.RegisterTitle.ToUpper();
            register1.Content = BahariModernUI.Resources.StringResources.Register;
            logout1.Text = BahariModernUI.Resources.StringResources.Logout.ToUpper();
            logout2.Text = BahariModernUI.Resources.StringResources.LogoutText;
            logout3.Content = BahariModernUI.Resources.StringResources.Logout;
            or.Text = BahariModernUI.Resources.StringResources.Or.ToUpper();
            full.Text = BahariModernUI.Resources.StringResources.Full + " : ";
        }

        private void Load(object sender, RoutedEventArgs e)
        {
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

            Reload();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (this.user.Text == "" || this.password.Password == "")
            {
                ModernDialog.ShowMessage(BahariModernUI.Resources.StringResources.LoginConfirmText, BahariModernUI.Resources.StringResources.LoginConfirm, MessageBoxButton.OK);

            }
            else
            {
                try
                {
                    SQLiteDatabase db = new SQLiteDatabase();
                    DataTable users;
                    String query = "SELECT NAME, PASSWORD ";
                    query += "FROM USER ";
                    query += "WHERE USERNAME = '" + this.user.Text.ToString() + "';";

                    //ModernDialog.ShowMessage(query, "", MessageBoxButton.OK);

                    users = db.GetDataTable(query);

                    String password = "";
                    String name = "";

                    // Or looped through for some other reason
                    foreach (DataRow r in users.Rows)
                    {
                        password = r["PASSWORD"].ToString();
                        //ModernDialog.ShowMessage(password, "", MessageBoxButton.OK);
                        name = r["NAME"].ToString();
                    }

                    if (password == this.password.Password)
                    {
                        app.LoginName = name;
                        app.LoginUser = this.user.Text;
                        ModernDialog.ShowMessage(BahariModernUI.Resources.StringResources.Success, BahariModernUI.Resources.StringResources.Login, MessageBoxButton.OK);
                        this.user.Text = "";
                        this.password.Password = "";
                        this.login.Visibility = System.Windows.Visibility.Hidden;
                        this.logout.Visibility = System.Windows.Visibility.Visible;
                        logindata(app.LoginUser);
                        Reload();

                    }
                    else
                    {
                        ModernDialog.ShowMessage(BahariModernUI.Resources.StringResources.Fail, BahariModernUI.Resources.StringResources.Login, MessageBoxButton.OK);
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
                ModernDialog.ShowMessage(BahariModernUI.Resources.StringResources.LoginConfirmText, BahariModernUI.Resources.StringResources.LoginConfirm, MessageBoxButton.OK);

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
                        ModernDialog.ShowMessage(BahariModernUI.Resources.StringResources.Success, BahariModernUI.Resources.StringResources.RegisterTitle, MessageBoxButton.OK);
                        this.login.Visibility = System.Windows.Visibility.Hidden;
                        this.logout.Visibility = System.Windows.Visibility.Visible;
                        logindata(app.LoginUser);
                        Reload();
                    }
                    else
                    {
                        ModernDialog.ShowMessage(BahariModernUI.Resources.StringResources.Fail, BahariModernUI.Resources.StringResources.RegisterTitle, MessageBoxButton.OK);
                    }
                }
                catch (Exception crap)
                {
                    MessageBox.Show(crap.Message);
                }
                
                data.Clear();
                data.Add("USERNAME", this.reguser.Text);
                data.Add("SCORE", "0");
                try
                {
                    db.Insert("SCORE", data);
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

            logindata("");

            ModernDialog.ShowMessage(BahariModernUI.Resources.StringResources.Success, BahariModernUI.Resources.StringResources.Logout, MessageBoxButton.OK);

            this.login.Visibility = System.Windows.Visibility.Visible;
            this.logout.Visibility = System.Windows.Visibility.Hidden;

        }

        private void Reload()
        {
            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                DataTable users;
                String query = "SELECT NAME, SCORE ";
                query += "FROM USER ";
                query += "WHERE USERNAME = '" + app.LoginUser + "';";

                users = db.GetDataTable(query);

                // Or looped through for some other reason
                foreach (DataRow r in users.Rows)
                {
                    this.name.Text = r["NAME"].ToString().ToUpper();
                    this.score.Text = r["SCORE"].ToString();
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
            ScoreViewModel scm = new ScoreViewModel();
            scorechart.DataContext = scm;
        }

        private void logindata(string who) {
            SQLiteDatabase db = new SQLiteDatabase();
            Dictionary<String, String> data = new Dictionary<String, String>();
            data.Add("LOGIN", who);

            string condition = "ID = 1";
            try
            {
                db.Update("SETTING", data, condition);
            }
            catch (Exception crap)
            {
                MessageBox.Show(crap.Message);
            }
        }
    }
}
