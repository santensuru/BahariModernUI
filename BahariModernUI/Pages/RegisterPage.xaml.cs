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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : UserControl
    {
        App app = (App)App.Current;
        public RegisterPage()
        {
            InitializeComponent();
            app.LoginName = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (app.LoginName == "")
            {
                this.layout.SelectedSource = new Uri("/Pages/Register/Login.xaml", UriKind.RelativeOrAbsolute);

                this.register.DisplayName = "Register";
                this.register.Source = new Uri("/Pages/Register/Register.xaml", UriKind.RelativeOrAbsolute);

                this.login.DisplayName = "login";
                this.login.Source = new Uri("/Pages/Register/Login.xaml", UriKind.RelativeOrAbsolute);
            }
            else
            {
                this.layout.SelectedSource = new Uri("/Pages/Register/Your.xaml", UriKind.RelativeOrAbsolute);

                this.register.DisplayName = app.LoginName;
                this.register.Source = new Uri("/Pages/Register/Your.xaml", UriKind.RelativeOrAbsolute);

                this.login.DisplayName = "logout";
                this.login.Source = new Uri("/Pages/Register/Logout.xaml", UriKind.RelativeOrAbsolute);
            }
        }
    }
}
