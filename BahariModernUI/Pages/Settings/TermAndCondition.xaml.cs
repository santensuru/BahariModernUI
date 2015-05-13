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

            tac.Text = "When You use this Software, You can copy it to other PC(s) without fee.\r\n";
            tac.Text += "You also can distributed it to other people, but you cannot comercial it.\r\n";
            tac.Text += "Before You copying to other, please send email to djuned.ong@gmail.com,\r\n for confirmation only.\r\n\r\n";
            tac.Text += "Thank You\r\n";

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton, this.CancelButton };
        }
    }
}
