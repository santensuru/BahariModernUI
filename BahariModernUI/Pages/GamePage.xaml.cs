using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
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

namespace BahariModernUI.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : UserControl
    {
        public GamePage()
        {
            InitializeComponent();

            webBrowser.Navigate("C:/Users/user/Documents/Visual Studio 2013/Projects/BahariModernUI/BahariModernUI/Pages/Game/FishyFlashGame.htm");
            //webBrowser.PreviewKeyDown += webBrowser_PreviewKeyDown;
            //webBrowser.KeyDown += webBrowser_KeyDown;
            webBrowser.Focusable = true;
            webBrowser.Focus();

            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(AppearanceManager.Current.AccentColor.ToString());

            detail.Foreground = brush;

            //System.Windows.Forms.HtmlDocument document = (System.Windows.Forms.HtmlDocument)webBrowser.Document;
            //document.Body.Style = "background-color:" + AppearanceManager.Current.AccentColor.ToString();
        }

        //void webBrowser_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    ModernDialog.ShowMessage(e.Key.ToString(), "", MessageBoxButton.OK);
        //    //throw new NotImplementedException();
        //}

        //void webBrowser_KeyDown(object sender, KeyEventArgs e)
        //{
        //    ModernDialog.ShowMessage(e.ToString(), "", MessageBoxButton.OK);
        //    //throw new NotImplementedException();
        //}

        private void Load(object sender, RoutedEventArgs e)
        {
            webBrowser.Navigate("C:/Users/user/Documents/Visual Studio 2013/Projects/BahariModernUI/BahariModernUI/Pages/Game/FishyFlashGame.htm");
            webBrowser.Focusable = true;
            webBrowser.Focus();

            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(AppearanceManager.Current.AccentColor.ToString());

            detail.Foreground = brush;

            //System.Windows.Forms.HtmlDocument document = (System.Windows.Forms.HtmlDocument)webBrowser.Document;
            //document.Body.Style = "background-color:" + AppearanceManager.Current.AccentColor.ToString();
        }
    }
}
