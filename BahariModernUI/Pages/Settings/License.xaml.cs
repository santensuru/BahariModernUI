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

namespace BahariModernUI.Pages.Settings
{
    /// <summary>
    /// Interaction logic for License.xaml
    /// </summary>
    public partial class License : ModernDialog
    {
        public License()
        {
            InitializeComponent();

            lice.Title = BahariModernUI.Resources.StringResources.License;
            license.Text = BahariModernUI.Resources.StringResources.LicenseText;

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton };
        }
    }
}
