using BahariModernUI.Model;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace BahariModernUI.Pages.Appendix
{
    /// <summary>
    /// Interaction logic for Reef.xaml
    /// </summary>
    public partial class Reef : UserControl
    {
        public Reef()
        {
            InitializeComponent();
            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                DataTable users;
                String query = "SELECT NAMA ";
                query += "FROM BIOTA ";
                query += "WHERE HABITAT = 2;"; // terumbu karang

                users = db.GetDataTable(query);

                List<BiotaModel> _biota = new List<BiotaModel>();

                // Or looped through for some other reason
                foreach (DataRow r in users.Rows)
                {
                    _biota.Add(new BiotaModel { Nama = r["NAMA"].ToString() });
                    //MessageBox.Show( _peoples.Count().ToString() );
                }

                tStack.ItemsSource = _biota;
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var _mainWindow = (MainWindow)Application.Current.MainWindow;

            TextBlock o = e.OriginalSource as TextBlock;
            //ModernDialog.ShowMessage(o.Text, "", MessageBoxButton.OK);

            Detail newdialoge = new Detail(o.Text);
            newdialoge.Owner = _mainWindow;
            newdialoge.ShowDialog();
        }

        //private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var _mainWindow = (MainWindow)Application.Current.MainWindow;

        //    string biota = (tStack.SelectedItem as BiotaModel).Nama.ToString();

        //    Detail newdialoge = new Detail(biota);
        //    newdialoge.Owner = _mainWindow;
        //    newdialoge.ShowDialog();
        //}
    }
}
