using BahariModernUI.Model;
using BahariModernUI.Pages.Appendix;
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

namespace BahariModernUI.Pages
{
    /// <summary>
    /// Interaction logic for FavoritePage.xaml
    /// </summary>
    public partial class FavoritePage : UserControl
    {
        List<BiotaModel> _biota;
        long count = -1;
        
        public FavoritePage()
        {
            InitializeComponent();
            _biota = new List<BiotaModel>();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var _mainWindow = (MainWindow)Application.Current.MainWindow;

            TextBlock o = e.OriginalSource as TextBlock;
            //ModernDialog.ShowMessage(o.Text, "", MessageBoxButton.OK);

            Detail newdialoge = new Detail(o.Text);
            newdialoge.Owner = _mainWindow;
            newdialoge.ShowDialog();

            Reload();
        }

        //private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var _mainWindow = (MainWindow)Application.Current.MainWindow;

        //    string biota = (tStack.SelectedItem as BiotaModel).Nama.ToString();
            
        //    Detail newdialoge = new Detail(biota);
        //    newdialoge.Owner = _mainWindow;
        //    newdialoge.ShowDialog();

        //    Reload();
        //}

        private void Load(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            //tStack.ItemsSource = null;
            _biota.Clear();
            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                DataTable users;
                String query = "SELECT NAMA ";
                query += "FROM BIOTA ";
                query += "WHERE FAVORIT = 1;";

                users = db.GetDataTable(query);

                // Or looped through for some other reason
                foreach (DataRow r in users.Rows)
                {
                    _biota.Add(new BiotaModel { Nama = r["NAMA"].ToString() });
                }

                if (count == -1)
                {
                    count = _biota.LongCount();
                    tStack.ItemsSource = _biota;
                }
                else if (count != _biota.LongCount())
                {
                    tStack.ItemsSource = null;
                    tStack.ItemsSource = _biota;
                }

                //if (_biota.LongCount() > 0)
                //    tStack.ItemsSource = _biota;
                //else
                //    tStack.ItemsSource = null;
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }
        }
    }
}
