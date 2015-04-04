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
    /// Interaction logic for PersonalityPage.xaml
    /// </summary>
    public partial class PersonalityPage : UserControl
    {
        App app = (App)App.Current;
        public PersonalityPage()
        {
            InitializeComponent();
            app.LoginName = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (app.LoginName == "")
            {
                this.layout.SelectedSource = new Uri("/Pages/Personality/Login.xaml", UriKind.RelativeOrAbsolute);

                this.personality.Source = new Uri("/Pages/Personality/Login.xaml", UriKind.RelativeOrAbsolute);
            }
            else
            {
                this.layout.SelectedSource = new Uri("/Pages/Personality/Logout.xaml", UriKind.RelativeOrAbsolute);

                this.personality.Source = new Uri("/Pages/Personality/Logout.xaml", UriKind.RelativeOrAbsolute);
            }
        }
    }
}
