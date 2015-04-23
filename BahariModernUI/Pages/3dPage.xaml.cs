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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        //bool lockedToggle = false;

        private Dictionary<string, Thread> threads = new Dictionary<string,Thread>();
        private PXCMSenseManager sm;
        private PXCMHandConfiguration _handConfig;

        int status = 0;
            
        public MapPage()
        {
            InitializeComponent();

            toggle.Content = "on";
            toggle.ToolTip = "You must have intel realsense camera to used this feature.";
            toggle.IsChecked = false;
            
            // if realsense not detected
            // lockedToggle = true;
            //toggle.IsEnabled = false;

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

            //Vector3DAnimation vAnimation = new
            //    Vector3DAnimation(new Vector3D(0, 0, -3),// new Vector3D(0, 3, 0),
            //    new Duration(TimeSpan.FromMilliseconds(1000)));
            //vAnimation.RepeatBehavior = RepeatBehavior.Forever;
            //myRotateTransform.Rotation.BeginAnimation(AxisAngleRotation3D.AxisProperty,
            //vAnimation);

            foo.Transform = myRotateTransform;


        }

        private void myView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //text.Text = myView.CurrentPosition.ToString();
            Console.WriteLine(myView.CurrentPosition.ToString());
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

        private void ToggleClick(object sender, RoutedEventArgs e)
        {
            if (toggle.Content.Equals("off"))
            {
                toggle.Content = "on";
                toggle.ToolTip = "You must have intel realsense camera to used this feature.";
                right.IsEnabled = true;
                left.IsEnabled = true;
                toggle.IsChecked = false;
            }
            else if (toggle.Content.Equals("on"))
            {
                toggle.Content = "off";
                toggle.ToolTip = "Turn off this feature.";
                right.IsEnabled = false;
                left.IsEnabled = false;
                toggle.IsChecked = true;
            }

            if (toggle.IsChecked == true)
            {
                // enable realsense
                // Create an instance of the SenseManager.
                sm = PXCMSenseManager.CreateInstance();

                //sm.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_COLOR, 640, 480, 30);

                // Enable hand tracking
                sm.EnableHand();

                // Get a hand instance here (or inside the AcquireFrame/ReleaseFrame loop) for querying features
                PXCMHandModule hand = sm.QueryHand();
                //MessageBox.Show("start");

                // Initialize the pipeline
                sm.Init();

                var handManager = sm.QueryHand();
                _handConfig = handManager.CreateActiveConfiguration();
                _handConfig.EnableGesture("thumb_up");
                _handConfig.EnableGesture("thumb_down");
                _handConfig.EnableGesture("spreadfingers");
                _handConfig.EnableGesture("v_sign");
                _handConfig.EnableAllAlerts();
                _handConfig.ApplyChanges();

                Thread thread = new Thread(HandRecognition);
                thread.Start();
                threads.Add("realsense", thread);

                Thread thread2 = new Thread(do_Rotate);
                thread2.Start();
                threads.Add("rotate", thread2);

                Thread.Sleep(5);
            }
            else
            {
                Thread thread = threads["realsense"];
                thread.Abort();

                _handConfig.Dispose();

                // Clean up
                sm.Dispose();

                Thread thread2 = threads["rotate"];
                thread2.Abort();

                threads.Clear();
                status = 0;
                Thread.Sleep(5);
            }
        }

        // thread
        //delegate void HandRecognitionCallback();
        private void HandRecognition()
        {
            
            
            // Stream data
            while (sm.AcquireFrame(true) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                //Console.WriteLine("test");

                // retrieve hand tracking results if ready
                PXCMHandModule hand2 = sm.QueryHand();
                if (hand2 != null)
                {
                    //Console.WriteLine("test");
                    
                    //MessageBox.Show("lepas");
                    //MessageBox.Show(hand2.ToString());
                    try
                    {
                        var handQuery = sm.QueryHand();
                        if (handQuery != null)
                        {
                            var handData = handQuery.CreateOutput(); // Get processing results
                            handData.Update();
                            //Console.WriteLine("test");

                            PXCMHandData.GestureData gestureData;
                            PXCMHandData.IHand iHand = null;
                            //PXCMImage himage;
                            //iHand.QuerySegmentationImage(out himage);

                            if (handData.IsGestureFired("thumb_down", out gestureData))
                            {
                                //MessageBox.Show("bad");
                                //MessageBox.Show(gestureData.ToString());
                                //Dispatcher.Invoke(ThumbDown);
                                Console.WriteLine("bad");
                            }
                            else if (handData.IsGestureFired("thumb_up", out gestureData))
                            {
                                //MessageBox.Show("good");
                                //Dispatcher.Invoke(ThumbUp);
                                Console.WriteLine("good");
                            }
                            else if (handData.IsGestureFired("spreadfingers", out gestureData))
                            {
                                //MessageBox.Show("open");
                                //Dispatcher.Invoke(ThumbUp);
                                //PXCMPointF32 image = iHand.QueryMassCenterImage();
                                //MessageBox.Show(image.x.ToString());

                                if (handData.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_RIGHT_HANDS, 0, out iHand) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                                {
                                    //	Position broadcasting
                                    PXCMPointF32 handPosition = iHand.QueryMassCenterImage();
                                    //MessageBox.Show(handPosition.x.ToString());
                                    //Vector3 handPositionFormatted = new Vector3(handPosition.x, handPosition.y, 0.0f);
                                    Application.Current.Dispatcher.Invoke(new Action(() =>
                                    {
                                        //Right_RepeatButton(this, null);
                                        status = 2;
                                    }));
                                    //Console.WriteLine("Right" + handPosition.x.ToString() + " " + handPosition.y.ToString());
                                }

                                if (handData.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_LEFT_HANDS, 0, out iHand) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                                {
                                    //	Position broadcasting
                                    PXCMPointF32 handPosition = iHand.QueryMassCenterImage();
                                    //MessageBox.Show(handPosition.x.ToString());
                                    //Vector3 handPositionFormatted = new Vector3(handPosition.x, handPosition.y, 0.0f);
                                    Application.Current.Dispatcher.Invoke(new Action(() =>
                                    {
                                        //Left_RepeatButton(this, null);
                                        status = 1;
                                    }));
                                    //Console.WriteLine("Left" + handPosition.x.ToString() + " " + handPosition.y.ToString());
                                    //MessageBox.Show("left");
                                }

                            }
                            else if (handData.IsGestureFired("v_sign", out gestureData))
                            {
                                //if (handData.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_RIGHT_HANDS, 0, out iHand) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                                //{
                                //    Console.WriteLine("wave");
                                //}
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    status = 0;
                                }));
                            }
                            //else
                            //{
                            //    Application.Current.Dispatcher.Invoke(new Action(() =>
                            //    {
                            //        //status = 0;
                            //    }));
                            //}
                            handData.Dispose();
                        }
                    }
                    finally
                    {
                        sm.ReleaseFrame();
                    }
                }

                //// resume next frame processing
                //sm.ReleaseFrame();
            }
        }

        private void do_Rotate()
        {
            int st=0;
            while (true)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    st = status;
                }));

                if (st == 1)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        ax3d.Angle += 1;
                        ax3d.Angle %= 360;
                    }));

                }
                else if (st == 2)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        ax3d.Angle -= 1;
                        ax3d.Angle %= 360;
                    }));

                }
                Thread.Sleep(40);
            }
        }

        private void myView_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Console.WriteLine(myView.CurrentPosition.ToString());
        }

        //static byte[] GetBytes(string str)
        //{
        //    byte[] bytes = new byte[str.Length * sizeof(char)];
        //    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        //    return bytes;
        //}


    }
}
