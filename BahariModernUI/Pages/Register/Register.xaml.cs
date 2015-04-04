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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.name.Text == "" || this.user.Text == "" || this.password.Password == "")
            {
                ModernDialog.ShowMessage("Please fill the blank name, username or password.", "Missing your name or password", MessageBoxButton.OK);

                //var v = new ModernDialog
                //{
                //    Title = "Error",
                //    Content = "Missing your name or password."
                //};
                //v.Show();
            }
            //MessageBox.Show(this.name.Text);
            //MessageBox.Show(this.password.Password);
            else
            {
                SQLiteDatabase db = new SQLiteDatabase();
                //DataTable id = db.GetDataTable("SELECT COALESCE(MAX(ID)+1, 0) FROM USER");
                //String idNow = "";
                //foreach (DataRow i in id.Rows)
                //{
                //    idNow = i["ID"].ToString();
                //}
                Dictionary<String, String> data = new Dictionary<String, String>();
                data.Add("NAME", this.name.Text);
                data.Add("USERNAME", this.user.Text);
                data.Add("PASSWORD", this.password.Password);
                try
                {
                    if (db.Insert("USER", data) == true)
                    {
                        ModernDialog.ShowMessage("Success :) .", "Registration", MessageBoxButton.OK);
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
                this.name.Text = "";
                this.user.Text = "";
                this.password.Password = "";
            }
        }


    }
}
