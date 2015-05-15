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
        
        public MainWindow()
        {
            InitializeComponent();

            // load localization and globalization
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("id-ID");

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

            app.LoginName = "";
            app.LoginUser = "";

            String color = "";
            String theme = "";
            String font = "";
            String login = "";
            String name = "";
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
                }
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }

            if (color != "" && theme != "" && font != "")
            {
                byte[] argb = new byte[4];
                    
                argb = StringToByteArray(color.Replace("#", ""));

                AppearanceManager.Current.AccentColor = Color.FromRgb(argb[1], argb[2], argb[3]);

                if (theme == "dark")
                    AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;

                if (font == "small")
                    AppearanceManager.Current.FontSize = FirstFloor.ModernUI.Presentation.FontSize.Small;
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

    }
}
