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

            about.Text = BahariModernUI.Resources.StringResources.About.ToUpper();
            about1.Text = BahariModernUI.Resources.StringResources.AboutText;
            joinVentureWith.Text = BahariModernUI.Resources.StringResources.Join.ToUpper();
            //joinVentureWith1.Text = "Direktorat Jendral Kelautan, Pesisir dan Pulau-pulau Kecil - Kementerian Kelautan dan Perikanan\r\n";
            joinVentureWith1.Text = "Yayasan Terumbu Karang Indonesia";
            poweredBy.Text = BahariModernUI.Resources.StringResources.Powered.ToUpper();
            poweredBy1.Text = "Bing Map\r\nHelix Toolkit\r\nIntel RealSense\r\nSQLite\r\nwww.ausarabexplore.info\r\nSparrow Chart";

            Run run1 = new Run(BahariModernUI.Resources.StringResources.Term + " ...");

            Hyperlink link1 = new Hyperlink(run1);
            link1.NavigateUri = new Uri("https://github.com/santensuru/BahariModernUI");
            termAndCondition.Text = BahariModernUI.Resources.StringResources.Term.ToUpper();
            termAndCondition1.Inlines.Add(link1);

            link1.RequestNavigate += (sender, e) =>
            {
                //System.Diagnostics.Process.Start(e.Uri.ToString());

                var _mainWindow = (MainWindow)Application.Current.MainWindow;
                
                TermAndCondition newdialoge = new TermAndCondition();
                newdialoge.Owner = _mainWindow;
                newdialoge.ShowDialog();
            };

            Run run = new Run(BahariModernUI.Resources.StringResources.License + " ...");

            Hyperlink link = new Hyperlink(run);
            link.NavigateUri = new Uri("https://github.com/santensuru/BahariModernUI");
            license.Text = BahariModernUI.Resources.StringResources.License.ToUpper();
            license1.Inlines.Add(link);

            link.RequestNavigate += (sender, e) =>
            {
                //System.Diagnostics.Process.Start(e.Uri.ToString());

                var _mainWindow = (MainWindow)Application.Current.MainWindow;

                License newdialoge = new License();
                newdialoge.Owner = _mainWindow;
                newdialoge.ShowDialog();
            };

            Run run2 = new Run("Developer Terangi Bahari");

            Hyperlink link2 = new Hyperlink(run2);
            link2.NavigateUri = new Uri("mailto:djuned.ong@gmail.com?subject=Terangi%20Bahari%20user%20feedback");
            feedback.Text = BahariModernUI.Resources.StringResources.Feedback.ToUpper();
            feedback1.Inlines.Add(link2);

            link2.RequestNavigate += (sender, e) =>
            {
                System.Diagnostics.Process.Start(e.Uri.ToString());
            };
        }
    }
}
