using BahariModernUI.Model;
using FirstFloor.ModernUI.Presentation;
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
            app.LoginName = "";

            String color = "";
            String theme = "";
            String font = "";
            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                DataTable biota;

                String query = "SELECT * ";
                query += "FROM SETTING;";
                
                biota = db.GetDataTable(query);

                //ModernDialog.ShowMessage(query, "", MessageBoxButton.OK);

                int i = 0;
                // Or looped through for some other reason
                foreach (DataRow r in biota.Rows)
                {
                    color = r["COLOR"].ToString();
                    theme = r["THEME"].ToString();
                    font = r["FONT"].ToString();
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
            
        }

        static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
