﻿using BahariModernUI.Model;
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

namespace BahariModernUI.Pages.Settings
{
    /// <summary>
    /// Interaction logic for Appearance.xaml
    /// </summary>
    public partial class Appearance : UserControl
    {
        public Appearance()
        {
            InitializeComponent();

            appearance.Text = BahariModernUI.Resources.StringResources.Appearance.ToUpper();
            theme.Text = BahariModernUI.Resources.StringResources.Theme + " : ";
            font.Text = BahariModernUI.Resources.StringResources.Font + " : ";
            language.Text = BahariModernUI.Resources.StringResources.Language + " : ";
            save.Content = BahariModernUI.Resources.StringResources.Save;

            // create and assign the appearance view model
            this.DataContext = new AppearanceViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string langFirst = "";
            
            SQLiteDatabase db = new SQLiteDatabase();
            DataTable biota;

            String query = "SELECT LANGUAGE ";
            query += "FROM SETTING;";

            biota = db.GetDataTable(query);

            //ModernDialog.ShowMessage(query, "", MessageBoxButton.OK);

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

            //ModernDialog.ShowMessage(data.ElementAt(0).Value + " " + data.ElementAt(1).Value + " " + data.ElementAt(2).Value, "Themes", MessageBoxButton.OK);

            string condition = "ID = 1;";
            try
            {
                if (db.Update("SETTING", data, condition) == true)
                {
                    //ModernDialog.ShowMessage(BahariModernUI.Resources.StringResources.LanguageSave, BahariModernUI.Resources.StringResources.Theme, MessageBoxButton.OK);
                    //System.Windows.Forms.Application.Restart();
                    //System.Windows.Application.Current.Shutdown();

                    var dlg = new ModernDialog
                    {
                        Title = BahariModernUI.Resources.StringResources.Theme,
                        Content = BahariModernUI.Resources.StringResources.Success + "\r\n\r\n" + BahariModernUI.Resources.StringResources.LanguageText
                    };
                    dlg.Buttons = new Button[] { dlg.OkButton };
                    dlg.ShowDialog();

                    var dlgInside = new ModernDialog
                    {
                        Title = BahariModernUI.Resources.StringResources.Warning,
                        Content = BahariModernUI.Resources.StringResources.Restart
                    };
                    dlgInside.YesButton.Content = BahariModernUI.Resources.StringResources.Yes;
                    dlgInside.NoButton.Content = BahariModernUI.Resources.StringResources.No;

                    dlgInside.Buttons = new Button[] { dlgInside.YesButton, dlgInside.NoButton };
                    SystemSounds.Beep.Play();
                    dlgInside.ShowDialog();

                    //MessageBox.Show(dlgInside.DialogResult.HasValue ? dlgInside.DialogResult.ToString() : "<null>");
                    //MessageBox.Show(dlgInside.MessageBoxResult.ToString());

                    Boolean temp = dlgInside.DialogResult.HasValue ? dlgInside.DialogResult.GetValueOrDefault() : false;

                    if (temp == true)
                    {
                        System.Windows.Forms.Application.Restart();
                        System.Windows.Application.Current.Shutdown();
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
    }
}
