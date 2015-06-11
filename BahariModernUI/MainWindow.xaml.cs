using BahariModernUI.Model;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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

namespace BahariModernUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        App app = (App)App.Current;
        DateTime start;
        DateTime end;
        
        public MainWindow()
        {
            InitializeComponent();

            start = DateTime.Now.ToLocalTime();

            app.LoginName = "";
            app.LoginUser = "";

            String color = "";
            String theme = "";
            String font = "";
            String login = "";
            String name = "";
            String language = "";
            String langFirst = "";
            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                DataTable biota;

                String query = "SELECT * ";
                query += "FROM SETTING;";
                
                biota = db.GetDataTable(query);

                //ModernDialog.ShowMessage(query, "", MessageBoxButton.OK);

                // Or looped through for some other reason
                foreach (DataRow r in biota.Rows)
                {
                    color = r["COLOR"].ToString();
                    theme = r["THEME"].ToString();
                    font = r["FONT"].ToString();
                    login = r["LOGIN"].ToString();
                    language = r["LANGUAGE"].ToString();
                    langFirst = r["LANGFIRST"].ToString();
                }
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }

            if (color != "" && theme != "" && font != "" && language != "")
            {
                byte[] argb = new byte[4];
                    
                argb = StringToByteArray(color.Replace("#", ""));

                AppearanceManager.Current.AccentColor = Color.FromRgb(argb[1], argb[2], argb[3]);

                //MessageBox.Show(langFirst);

                if (langFirst == "bahasa")
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("id-ID");
                else
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US"); 
                
                if (theme == BahariModernUI.Resources.StringResources.Dark)
                    AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;

                if (font == BahariModernUI.Resources.StringResources.Small)
                    AppearanceManager.Current.FontSize = FirstFloor.ModernUI.Presentation.FontSize.Small;

                if (language == "bahasa")
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("id-ID");
                else
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US"); 
            }

            if (login != "")
            {
                app.LoginUser = login;

                try
                {
                    SQLiteDatabase db = new SQLiteDatabase();
                    DataTable users;
                    String query = "SELECT NAME \"Name\" ";
                    query += "FROM USER ";
                    query += "WHERE USERNAME = '" + login + "';";

                    users = db.GetDataTable(query);

                    // Or looped through for some other reason
                    foreach (DataRow r in users.Rows)
                    {
                        name = r["Name"].ToString();
                    }
                }
                catch (Exception fail)
                {
                    String error = "The following error has occurred:\n\n";
                    error += fail.Message.ToString() + "\n\n";
                    MessageBox.Show(error);
                }

                app.LoginName = name;

                //MessageBox.Show(name);
            }

            main.DisplayName = BahariModernUI.Resources.StringResources.Home;
            main1.DisplayName = BahariModernUI.Resources.StringResources.Home;
            map.DisplayName = BahariModernUI.Resources.StringResources.Map;
            experience.DisplayName = BahariModernUI.Resources.StringResources.Experience;
            appendix.DisplayName = BahariModernUI.Resources.StringResources.Appendix;
            favorite.DisplayName = BahariModernUI.Resources.StringResources.Favorite;
            game.DisplayName = BahariModernUI.Resources.StringResources.Game;
            settings.DisplayName = BahariModernUI.Resources.StringResources.Settings;
            settings1.DisplayName = BahariModernUI.Resources.StringResources.Settings;
            software.DisplayName = BahariModernUI.Resources.StringResources.Software;
            personality.DisplayName = BahariModernUI.Resources.StringResources.Personality;
            personality1.DisplayName = BahariModernUI.Resources.StringResources.Personality;
            personality2.DisplayName = BahariModernUI.Resources.StringResources.Personality;
        }

        static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private void ModernWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Right - this.Width) / 2;
            this.Top = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            //this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
        }

        private void ModernWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //try
            //{
            //    System.Windows.Application.Current.Shutdown();
            //    //this.Owner.Show();
            //}
            //catch (Exception ex)
            //{
            //    //nop
            //}

            end = DateTime.Now.ToLocalTime();

            if (app.LoginUser != "")
            {
                // Insert Last Score
                SQLiteDatabase db = new SQLiteDatabase();
                Dictionary<String, String> data = new Dictionary<String, String>();
                data.Add("USERNAME", app.LoginUser);
                data.Add("SCORE", end.Subtract(start).TotalMinutes.ToString());
                try
                {
                    db.Insert("SCORE", data);
                }
                catch (Exception crap)
                {
                    MessageBox.Show(crap.Message);
                }

                // Get Average Score
                DataTable users;
                String query = "SELECT AVG( SCORE ) AS AVERAGE ";
                query += "FROM SCORE ";
                query += "WHERE USERNAME = '" + app.LoginUser + "';";
                users = db.GetDataTable(query);
                String scoreAvg = "";
                foreach (DataRow r in users.Rows)
                {
                    scoreAvg = r["AVERAGE"].ToString();
                }

                // Insert Average Score
                data.Clear();
                data.Add("SCORE", scoreAvg);
                string condition = "USERNAME LIKE '%" + app.LoginUser + "%'";
                db.Update("USER", data, condition);
            }

            //Console.WriteLine(end.Subtract(start).TotalMinutes);
        }

    }
}
