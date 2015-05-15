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

namespace BahariModernUI.Pages.Settings
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();

            this.about.Text = "Terangi Bahari is an application for educating people about Indonesian water habitat.";
            this.joinVentureWith.Text = "Direktorat Jendral Kelautan, Pesisir dan Pulau-pulau Kecil - Kementerian Kelautan dan Perikanan\nYayasan Terumbu Karang Indonesia";
            this.poweredBy.Text = "Bing Map\nHelix Toolkit\nIntel RealSense\nSQLite\nwww.ausarabexplore.info";

            Run run1 = new Run("Term And Condition ...");

            Hyperlink link1 = new Hyperlink(run1);
            link1.NavigateUri = new Uri("https://github.com/santensuru/BahariModernUI");
            termAndCondition.Inlines.Add(link1);

            link1.RequestNavigate += (sender, e) =>
            {
                //System.Diagnostics.Process.Start(e.Uri.ToString());

                var _mainWindow = (MainWindow)Application.Current.MainWindow;
                
                TermAndCondition newdialoge = new TermAndCondition();
                newdialoge.Owner = _mainWindow;
                newdialoge.ShowDialog();
            };
            
            Run run = new Run("MIT License");

            Hyperlink link = new Hyperlink(run);
            link.NavigateUri = new Uri("https://github.com/santensuru/BahariModernUI/blob/master/README.md#mit-license");
            license.Inlines.Add(link);

            link.RequestNavigate += (sender, e) =>
            {
                System.Diagnostics.Process.Start(e.Uri.ToString());
            };

            Run run2 = new Run("Developer Terangi Bahari");

            Hyperlink link2 = new Hyperlink(run2);
            link2.NavigateUri = new Uri("mailto:djuned.ong@gmail.com?subject=Terangi%20Bahari%20user%20feedback");
            feedback.Inlines.Add(link2);

            link2.RequestNavigate += (sender, e) =>
            {
                System.Diagnostics.Process.Start(e.Uri.ToString());
            };
        }
    }
}
