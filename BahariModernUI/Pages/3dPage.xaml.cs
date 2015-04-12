using BahariModernUI.Model;
using FirstFloor.ModernUI.Windows.Controls;
using HelixToolkit.Wpf;
using HelixToolkit.Wpf.SharpDX;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BahariModernUI.Pages
{
    /// <summary>
    /// Interaction logic for MapPage.xaml
    /// </summary>
    public partial class MapPage : UserControl
    {

        AxisAngleRotation3D ax3d;
        byte[] stream;
            
        public MapPage()
        {
            InitializeComponent();

            myView.Camera = new System.Windows.Media.Media3D.OrthographicCamera { Position = new Point3D(0, -10000, 0), LookDirection = new Vector3D(0, -1000, 0), UpDirection = new Vector3D(0, 0, 1000) };
            myView.ShowFrameRate = true;
            myView.IsRotationEnabled = false;
            myView.IsMoveEnabled = false;

            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                DataTable biota;

                String query = "SELECT * ";
                query += "FROM OBJEK ";
                query += "WHERE NAMA LIKE '%Tuna%';";

                biota = db.GetDataTable(query);

                //ModernDialog.ShowMessage(query, "", MessageBoxButton.OK);

                // Or looped through for some other reason
                foreach (DataRow r in biota.Rows)
                {
                    //string temp = r["DATA"].ToString();
                    //stream = new byte[temp.Length * sizeof(char)];
                    //stream = GetBytes(temp);
                    
                    stream = new byte[((byte[])r["DATA"]).Length];
                    stream = ((byte[])r["DATA"]);
                    //ModernDialog.ShowMessage(stream.ToString(), "", MessageBoxButton.OK);
                }
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }

            Stream streams = new MemoryStream(stream);

            //background.AlignmentX = AlignmentX.Right;

            //var mi = new ModelImporter();
            //System.Windows.Media.Media3D.Model3DGroup currentModel = mi.Load("C:\\Users\\user\\Downloads\\Compressed\\1\\Amago.max", null, true);

            //// Display the model
            //foo.Content = currentModel;

            //HelixToolkit.Wpf.ObjReader CurrentHelixObjReader = new HelixToolkit.Wpf.ObjReader();

            //Uri uri = new Uri("C:/Users/user/Downloads/Compressed/Tuna/Tuna/TUNA.obj", UriKind.Relative);
            //Uri uri = new Uri("C:/Users/user/Downloads/Compressed/jelly fish series jelly fish one/jellyfish series jelly fish one improved.obj", UriKind.Relative);
            //ModernDialog.ShowMessage(uri.ToString(), "", MessageBoxButton.OK);

            //System.Windows.Media.Media3D.Model3DGroup MyModel = CurrentHelixObjReader.Read(streams);

            HelixToolkit.Wpf.StudioReader CurrentHelix3DSStudioReader = new HelixToolkit.Wpf.StudioReader();
            //Uri uri = new Uri("C:/Users/user/Downloads/Compressed/Tuna/Tuna/TUNA.3ds", UriKind.Relative);

            System.Windows.Media.Media3D.Model3DGroup MyModel = CurrentHelix3DSStudioReader.Read(streams);

            // Display the model
            foo.Content = MyModel;

            ax3d = new AxisAngleRotation3D(new Vector3D(0, 0, 3), 180); // 0bj -> 0 2 0
            RotateTransform3D myRotateTransform = new RotateTransform3D(ax3d);
            foo.Transform = myRotateTransform;

        }

        private void myView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //text.Text = myView.CurrentPosition.ToString();
        }

        private void txtUserEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                //ModernDialog.ShowMessage(txtUserEntry.Text.ToString(), "", MessageBoxButton.OK);
                stream = null;

                try
                {
                    SQLiteDatabase db = new SQLiteDatabase();
                    DataTable biota;

                    String query = "SELECT * ";
                    query += "FROM OBJEK ";
                    query += "WHERE NAMA LIKE '%" + txtUserEntry.Text.ToString() + "%';";

                    biota = db.GetDataTable(query);

                    // Or looped through for some other reason
                    foreach (DataRow r in biota.Rows)
                    {
                        stream = new byte[((byte[])r["DATA"]).Length];
                        stream = ((byte[])r["DATA"]);
                    }
                }
                catch (Exception fail)
                {
                    String error = "The following error has occurred:\n\n";
                    error += fail.Message.ToString() + "\n\n";
                    MessageBox.Show(error);
                }

                if (stream == null)
                {
                    ModernDialog.ShowMessage(txtUserEntry.Text.ToString() + " not found.", "3D Experience", MessageBoxButton.OK);
                }
                else
                {

                    Stream streams = new MemoryStream(stream);

                    HelixToolkit.Wpf.StudioReader CurrentHelix3DSStudioReader = new HelixToolkit.Wpf.StudioReader();

                    System.Windows.Media.Media3D.Model3DGroup MyModel = CurrentHelix3DSStudioReader.Read(streams);

                    // Display the model
                    foo.Content = MyModel;
                }

            }
        }

        //private void Left_Button(object sender, RoutedEventArgs e)
        //{
        //    ax3d.Angle += 30;
        //    ax3d.Angle %= 360;
        //}

        //private void Right_Button(object sender, RoutedEventArgs e)
        //{
        //    ax3d.Angle -= 30;
        //    ax3d.Angle %= 360;
        //}

        private void Left_RepeatButton(object sender, RoutedEventArgs e)
        {
            ax3d.Angle += 10;
            ax3d.Angle %= 360;
        }

        private void Right_RepeatButton(object sender, RoutedEventArgs e)
        {
            ax3d.Angle -= 10;
            ax3d.Angle %= 360;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            ax3d.Angle = 180;
        }


        //static byte[] GetBytes(string str)
        //{
        //    byte[] bytes = new byte[str.Length * sizeof(char)];
        //    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        //    return bytes;
        //}


    }
}
