using BahariModernUI.Model;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BahariModernUI.Pages.Settings
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class AppearanceViewModel
        : NotifyPropertyChanged
    {
        private string FontSmall = BahariModernUI.Resources.StringResources.Small;
        private string FontLarge = BahariModernUI.Resources.StringResources.Large;

        private string English = "english";
        private string Bahasa = "bahasa";

        // 9 accent colors from metro design principles
        /*private Color[] accentColors = new Color[]{
            Color.FromRgb(0x33, 0x99, 0xff),   // blue
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x33, 0x99, 0x33),   // green
            Color.FromRgb(0x8c, 0xbf, 0x26),   // lime
            Color.FromRgb(0xf0, 0x96, 0x09),   // orange
            Color.FromRgb(0xff, 0x45, 0x00),   // orange red
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xff, 0x00, 0x97),   // magenta
            Color.FromRgb(0xa2, 0x00, 0xff),   // purple            
        };*/

        // 20 accent colors from Windows Phone 8
        private Color[] accentColors = new Color[]{
            Color.FromRgb(0xa4, 0xc4, 0x00),   // lime
            Color.FromRgb(0x60, 0xa9, 0x17),   // green
            Color.FromRgb(0x00, 0x8a, 0x00),   // emerald
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x1b, 0xa1, 0xe2),   // cyan
            Color.FromRgb(0x00, 0x50, 0xef),   // cobalt
            Color.FromRgb(0x6a, 0x00, 0xff),   // indigo
            Color.FromRgb(0xaa, 0x00, 0xff),   // violet
            Color.FromRgb(0xf4, 0x72, 0xd0),   // pink
            Color.FromRgb(0xd8, 0x00, 0x73),   // magenta
            Color.FromRgb(0xa2, 0x00, 0x25),   // crimson
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xfa, 0x68, 0x00),   // orange
            Color.FromRgb(0xf0, 0xa3, 0x0a),   // amber
            Color.FromRgb(0xe3, 0xc8, 0x00),   // yellow
            Color.FromRgb(0x82, 0x5a, 0x2c),   // brown
            Color.FromRgb(0x6d, 0x87, 0x64),   // olive
            Color.FromRgb(0x64, 0x76, 0x87),   // steel
            Color.FromRgb(0x76, 0x60, 0x8a),   // mauve
            Color.FromRgb(0x87, 0x79, 0x4e),   // taupe
        };

        private Color selectedAccentColor;
        private LinkCollection themes = new LinkCollection();
        private Link selectedTheme;
        private string selectedFontSize;
        private string selectedLanguage;

        public AppearanceViewModel()
        {
            // add the default themes
            this.themes.Add(new Link { DisplayName = BahariModernUI.Resources.StringResources.Dark, Source = AppearanceManager.DarkThemeSource });
            this.themes.Add(new Link { DisplayName = BahariModernUI.Resources.StringResources.Light, Source = AppearanceManager.LightThemeSource });

            this.SelectedFontSize = AppearanceManager.Current.FontSize == FontSize.Large ? FontLarge : FontSmall;

            //MessageBox.Show(System.Threading.Thread.CurrentThread.CurrentUICulture.ToString());

            //this.SelectedLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en-US" ? English : Bahasa;
            this.SelectedLanguage = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString() == "en-US" ? English : Bahasa;
            SyncThemeAndColor();

            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }

        private void SyncThemeAndColor()
        {
            // synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.
            this.SelectedTheme = this.themes.FirstOrDefault(l => l.Source.Equals(AppearanceManager.Current.ThemeSource));

            // and make sure accent color is up-to-date
            this.SelectedAccentColor = AppearanceManager.Current.AccentColor;

            //// save themes
            //GoSaveThemes();
        }

        //private void GoSaveThemes()
        //{
        //    SQLiteDatabase db = new SQLiteDatabase();

        //    Dictionary<String, String> data = new Dictionary<String, String>();
        //    data.Add("THEME", this.SelectedTheme.DisplayName);
        //    data.Add("COLOR", this.SelectedAccentColor.ToString());
        //    data.Add("FONT", this.SelectedFontSize);

        //    ModernDialog.ShowMessage(data.ElementAt(0).Value + " " + data.ElementAt(1).Value + " " + data.ElementAt(2).Value, "Themes", MessageBoxButton.OK);

        //    string condition = "WHERE ID = 1;";
        //    try
        //    {
        //        if (db.Update("SETTING", data, condition) == true)
        //        {
        //            ModernDialog.ShowMessage("Saved :) .", "Themes", MessageBoxButton.OK);
        //        }
        //        else
        //        {
        //            ModernDialog.ShowMessage("Failed :( , Please try again.", "Themes", MessageBoxButton.OK);
        //        }
        //    }
        //    catch (Exception crap)
        //    {
        //        MessageBox.Show(crap.Message);
        //    }

        //}

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
            {
                SyncThemeAndColor();
            }
        }

        public LinkCollection Themes
        {
            get { return this.themes; }
        }

        public string[] FontSizes
        {
            get { return new string[] { FontSmall, FontLarge }; }
        }

        public string[] Languages
        {
            get { return new string[] { English, Bahasa }; }
        }

        public Color[] AccentColors
        {
            get { return this.accentColors; }
        }

        public Link SelectedTheme
        {
            get { return this.selectedTheme; }
            set
            {
                if (this.selectedTheme != value)
                {
                    this.selectedTheme = value;
                    OnPropertyChanged("SelectedTheme");

                    // and update the actual theme
                    AppearanceManager.Current.ThemeSource = value.Source;
                }
            }
        }

        public string SelectedFontSize
        {
            get { return this.selectedFontSize; }
            set
            {
                if (this.selectedFontSize != value)
                {
                    this.selectedFontSize = value;
                    OnPropertyChanged("SelectedFontSize");

                    AppearanceManager.Current.FontSize = value == FontLarge ? FontSize.Large : FontSize.Small;
                }
            }
        }

        public string SelectedLanguage
        {
            get { return this.selectedLanguage; }
            set
            {
                if (this.selectedLanguage != value)
                {
                    this.selectedLanguage = value;
                    OnPropertyChanged("SelectedLanguage");

                    //System.Threading.Thread.CurrentThread.CurrentCulture = value == English ? new CultureInfo("en-US") : new CultureInfo("id-ID");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = value == English ? new CultureInfo("en-US") : new CultureInfo("id-ID");
                    //CultureInfo.DefaultThreadCurrentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
                    
                    //MessageBox.Show(System.Threading.Thread.CurrentThread.CurrentUICulture.ToString());

                }
            }
        }

        public Color SelectedAccentColor
        {
            get { return this.selectedAccentColor; }
            set
            {
                if (this.selectedAccentColor != value)
                {
                    this.selectedAccentColor = value;
                    OnPropertyChanged("SelectedAccentColor");

                    AppearanceManager.Current.AccentColor = value;
                }
            }
        }
    }
}
