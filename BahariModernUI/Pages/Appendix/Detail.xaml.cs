using BahariModernUI.Model;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : ModernDialog
    {
        public Detail()
        {
            InitializeComponent();

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton, this.CancelButton };
        }

        public Detail(String param)
        {
            InitializeComponent();

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton };

            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                DataTable biota;
                String query = "SELECT * ";
                query += "FROM BIOTA ";
                query += "WHERE NAMA LIKE '%" + param + "%';";

                biota = db.GetDataTable(query);

                //ModernDialog.ShowMessage(query, "", MessageBoxButton.OK);


                // Or looped through for some other reason
                foreach (DataRow r in biota.Rows)
                {
                    this.Title = r["NAMA"].ToString();
                    //ModernDialog.ShowMessage(this.Title, "", MessageBoxButton.OK);
                    image.Source = BitmapImageFromBytes((byte[])r["GAMBAR"]);
                    //ModernDialog.ShowMessage(r["GAMBAR"].ToString(), "", MessageBoxButton.OK);
                    persebaran.Text = r["PERSEBARAN"].ToString();
                    keterangan.Text = r["KETERANGAN"].ToString();

                    if (r["FAVORIT"].ToString() == "1")
                    {
                        minus.Visibility = System.Windows.Visibility.Visible;
                        plus.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        minus.Visibility = System.Windows.Visibility.Hidden;
                        plus.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }

            //this.dialog.Text = "Coba parameter => " + param;
        }
        
        static BitmapImage BitmapImageFromBytes(byte[] bytes)
        {
            BitmapImage image = null;
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(bytes);
                stream.Seek(0, SeekOrigin.Begin);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                image = new BitmapImage();
                image.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                image.StreamSource = ms;
                image.StreamSource.Seek(0, SeekOrigin.Begin);
                image.EndInit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
            return image;
        }

        private void Plus(object sender, RoutedEventArgs e)
        {
            minus.Visibility = System.Windows.Visibility.Visible;
            plus.Visibility = System.Windows.Visibility.Hidden;

            SQLiteDatabase db = new SQLiteDatabase();
            Dictionary<String, String> data = new Dictionary<String, String>();
            data.Add("FAVORIT", "1");

            string condition = "NAMA LIKE '%" + this.Title + "%'";
            try
            {
                if (db.Update("BIOTA", data, condition) == true)
                {
                    ModernDialog.ShowMessage("Set as favorite succesfully.", "Favorite", MessageBoxButton.OK);
                }
                else
                {
                    ModernDialog.ShowMessage("Set as favorite failed.", "Favorite", MessageBoxButton.OK);
                }
            }
            catch (Exception crap)
            {
                MessageBox.Show(crap.Message);
            }
        }

        private void Minus(object sender, RoutedEventArgs e)
        {
            minus.Visibility = System.Windows.Visibility.Hidden;
            plus.Visibility = System.Windows.Visibility.Visible;

            SQLiteDatabase db = new SQLiteDatabase();
            Dictionary<String, String> data = new Dictionary<String, String>();
            data.Add("FAVORIT", "0");

            string condition = "NAMA LIKE '%" + this.Title + "%'";
            try
            {
                if (db.Update("BIOTA", data, condition) == true)
                {
                    ModernDialog.ShowMessage("Remove from favorite succesfully.", "Favorite", MessageBoxButton.OK);
                }
                else
                {
                    ModernDialog.ShowMessage("Remove from favorite failed.", "Favorite", MessageBoxButton.OK);
                }
            }
            catch (Exception crap)
            {
                MessageBox.Show(crap.Message);
            }
        }
    }
}
