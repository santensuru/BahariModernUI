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
    /// Interaction logic for TermAndCondition.xaml
    /// </summary>
    public partial class TermAndCondition : ModernDialog
    {
        public TermAndCondition()
        {
            InitializeComponent();

            term.Title = BahariModernUI.Resources.StringResources.Term;
            tac.Text = BahariModernUI.Resources.StringResources.TermText;

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton };
        }
    }
}
