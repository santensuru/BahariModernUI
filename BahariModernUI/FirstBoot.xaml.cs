using BahariModernUI.Model;
using BahariModernUI.Pages.Settings;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Media;
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
    /// Interaction logic for FirstBoot.xaml
    /// </summary>
    public partial class FirstBoot : ModernDialog
    {
        public FirstBoot()
        {
            InitializeComponent();

            theme.Text = BahariModernUI.Resources.StringResources.Theme + " : ";
            font.Text = BahariModernUI.Resources.StringResources.Font + " : ";
            language.Text = BahariModernUI.Resources.StringResources.Language + " : ";
            save.Content = BahariModernUI.Resources.StringResources.Save;

            // create and assign the appearance view model
            this.DataContext = new AppearanceViewModel();

            this.Buttons = new Button[] { };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string langFirst = "";
            
            SQLiteDatabase db = new SQLiteDatabase();
            DataTable biota;

            String query = "SELECT LANGUAGE ";
            query += "FROM SETTING;";

            biota = db.GetDataTable(query);

            // Or looped through for some other reason
            foreach (DataRow r in biota.Rows)
            {
                langFirst = r["LANGUAGE"].ToString();
            }

            Dictionary<String, String> data = new Dictionary<String, String>();

            var a = theme1.SelectedItem as Link;

            data.Add("THEME", a.DisplayName.ToString());
            data.Add("COLOR", brush.SelectedItem.ToString());
            data.Add("FONT", font1.SelectedItem.ToString());
            data.Add("LANGUAGE", language1.SelectedItem.ToString());
            data.Add("LANGFIRST", langFirst);
            data.Add("FIRSTBOOT", "1");

            //ModernDialog.ShowMessage(data.ElementAt(0).Value + " " + data.ElementAt(1).Value + " " + data.ElementAt(2).Value, "Themes", MessageBoxButton.OK);

            string condition = "ID = 1;";
            try
            {
                if (db.Update("SETTING", data, condition) == true)
                {

                    var dlgInside = new ModernDialog
                    {
                        Title = BahariModernUI.Resources.StringResources.Theme,
                        Content = BahariModernUI.Resources.StringResources.FirstBoot
                    };
                    dlgInside.YesButton.Content = BahariModernUI.Resources.StringResources.Yes;
                    dlgInside.NoButton.Content = BahariModernUI.Resources.StringResources.No;

                    dlgInside.Buttons = new Button[] { dlgInside.YesButton, dlgInside.NoButton };
                    SystemSounds.Beep.Play();
                    dlgInside.ShowDialog();

                    Boolean temp = dlgInside.DialogResult.HasValue ? dlgInside.DialogResult.GetValueOrDefault() : false;

                    if (temp == true)
                    {
                        //System.Windows.Forms.Application.Restart();
                        //System.Windows.Application.Current.Shutdown();
                        MainWindow mainView = new MainWindow();
                        mainView.Owner = this;
                        this.Hide();
                        mainView.Show();
                    }
                }
                else
                {
                    ModernDialog.ShowMessage(BahariModernUI.Resources.StringResources.Fail, BahariModernUI.Resources.StringResources.Theme, MessageBoxButton.OK);
                }
            }
            catch (Exception crap)
            {
                MessageBox.Show(crap.Message);
            }
        }

        //private void FirstBoot_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Right - this.Width) / 2;
        //    this.Top = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;

        //    //MessageBox.Show(this.Left.ToString() + " " + this.Top.ToString());
        //}

    }
}