using BahariModernUI.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BahariModernUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public String LoginUser { set; get; }
        public String LoginName { set; get; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //add some bootstrap or startup logic 

            String firstBoot = "";
            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                DataTable biota;

                String query = "SELECT * ";
                query += "FROM SETTING;";

                biota = db.GetDataTable(query);

                // Or looped through for some other reason
                foreach (DataRow r in biota.Rows)
                {
                    firstBoot = r["FIRSTBOOT"].ToString();
                }
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }

            if (firstBoot == "0")
            {
                FirstBoot setting = new FirstBoot();
                setting.Show();
            }
            else
            {
                MainWindow mainView = new MainWindow();
                mainView.Show();
            }
        }
    }
}
