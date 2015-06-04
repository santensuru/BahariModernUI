using BahariModernUI.Model;
using FirstFloor.ModernUI.Presentation;
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
        String stream;

        private Dictionary<string, Thread> threads = new Dictionary<string,Thread>();
        private PXCMSenseManager sm;
        private PXCMHandConfiguration _handConfig;

        int status = 0;
        String toggleContent;
        String fishActive = "Tuna";
        byte[] active = new byte[4];
            
        public MapPage()
        {
            InitializeComponent();

            //search.Text = BahariModernUI.Resources.StringResources.Search;
            left.ToolTip = BahariModernUI.Resources.StringResources.Left;
            right.ToolTip = BahariModernUI.Resources.StringResources.Right;

            tuna.IsEnabled = false;
            tuna.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));
            tuna.ToolTip = "Tuna";
            salmon.ToolTip = "Salmon";

            toggleContent = BahariModernUI.Resources.StringResources.On;
            if (AppearanceManager.Current.ThemeSource == AppearanceManager.DarkThemeSource)
            {
                toggle1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.camera on.png"));
            }
            else
            {
                toggle1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/camera on.png"));
            }
            toggle.ToolTip = BahariModernUI.Resources.StringResources.OnLabel;
            toggle.IsChecked = false;
            
            // if realsense not detected
            // lockedToggle = true;
            //toggle.IsEnabled = false;
            
            // --> handled with try catch

            myView.Camera = new System.Windows.Media.Media3D.OrthographicCamera { Position = new Point3D(0, -10000, 0), LookDirection = new Vector3D(0, -1000, 0), UpDirection = new Vector3D(0, 0, 1000) };
            //myView.ShowFrameRate = true;
            myView.IsRotationEnabled = false;
            myView.IsMoveEnabled = false;

            ax3d = new AxisAngleRotation3D(new Vector3D(0, 0, 3), 180); // 0bj -> 0 2 0
            RotateTransform3D myRotateTransform = new RotateTransform3D(ax3d);

            Reload();

            foo.Transform = myRotateTransform;
        }

        //private void txtUserEntry_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Return)
        //    {
        //        //ModernDialog.ShowMessage(txtUserEntry.Text.ToString(), "", MessageBoxButton.OK);
        //        stream = null;

        //        fishActive = txtUserEntry.Text.ToString();
        //        Reload();

        //    }
        //}

        private void Tuna_Button(object sender, RoutedEventArgs e)
        {
            fishActive = "Tuna";
            tuna.IsEnabled = false;
            tuna.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));
            salmon.IsEnabled = true;
            salmon.ClearValue(Button.BackgroundProperty);
            Reload();
        }

        private void Salmon_Button(object sender, RoutedEventArgs e)
        {
            fishActive = "Salmon";
            salmon.IsEnabled = false;
            salmon.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));
            tuna.IsEnabled = true;
            tuna.ClearValue(Button.BackgroundProperty);
            Reload();
        }

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

        private void Reload()
        {
            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                DataTable biota;

                String query = "SELECT * ";
                query += "FROM OBJEK ";
                query += "WHERE NAMA LIKE '%" + fishActive + "%';";

                biota = db.GetDataTable(query);

                // Or looped through for some other reason
                foreach (DataRow r in biota.Rows)
                {
                    stream = r["DATA"].ToString();

                    state.Content = r["NAMA"];
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
                ModernDialog.ShowMessage(fishActive + " " + BahariModernUI.Resources.StringResources.NotFound, BahariModernUI.Resources.StringResources.Experience, MessageBoxButton.OK);
            }
            else
            {
                Uri uri = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "3dAssets\\" + stream);
                String streams = uri.OriginalString;

                //MessageBox.Show(streams);

                HelixToolkit.Wpf.StudioReader CurrentHelix3DSStudioReader = new HelixToolkit.Wpf.StudioReader();
                System.Windows.Media.Media3D.Model3DGroup MyModel = CurrentHelix3DSStudioReader.Read(streams);

                // Display the model
                foo.Content = MyModel;

            }

            ax3d.Angle = 180;
            myView.ZoomExtents();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            ax3d.Angle = 180;

            if (AppearanceManager.Current.ThemeSource == AppearanceManager.DarkThemeSource)
            {
                left1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.rotate left.png"));
                right1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.rotate right.png"));

                if (toggleContent.Equals(BahariModernUI.Resources.StringResources.Off))
                    toggle1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.camera off.png"));
                else
                    toggle1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.camera on.png"));

                tuna1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.tuna.png"));
                salmon1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.salmon.png"));
            }
            else
            {
                left1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/rotate left.png"));
                right1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/rotate right.png"));

                if (toggleContent.Equals(BahariModernUI.Resources.StringResources.Off))
                    toggle1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/camera off.png"));
                else
                    toggle1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/camera on.png"));

                tuna1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/tuna.png"));
                salmon1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/salmon.png"));
            }

            active = StringToByteArray(AppearanceManager.Current.AccentColor.ToString().Replace("#", ""));
            if (fishActive == "Tuna")
            {
                tuna.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));
            }
            else if (fishActive == "Salmon")
            {
                salmon.Background = new SolidColorBrush(Color.FromArgb(active[0], active[1], active[2], active[3]));
            }
        }

        private void ToggleClick(object sender, RoutedEventArgs e)
        {
            if (toggleContent.Equals(BahariModernUI.Resources.StringResources.Off))
            {
                toggleContent = BahariModernUI.Resources.StringResources.On;
                if (AppearanceManager.Current.ThemeSource == AppearanceManager.DarkThemeSource)
                {
                    toggle1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.camera on.png"));
                }
                else
                {
                    toggle1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/camera on.png"));
                }
                toggle.ToolTip = BahariModernUI.Resources.StringResources.OnLabel;
                right.IsEnabled = true;
                left.IsEnabled = true;
                toggle.IsChecked = false;
            }
            else if (toggleContent.Equals(BahariModernUI.Resources.StringResources.On))
            {
                toggleContent = BahariModernUI.Resources.StringResources.Off;
                if (AppearanceManager.Current.ThemeSource == AppearanceManager.DarkThemeSource)
                {
                    toggle1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.camera off.png"));
                }
                else
                {
                    toggle1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/camera off.png"));
                }
                toggle.ToolTip = BahariModernUI.Resources.StringResources.OffLabel;
                right.IsEnabled = false;
                left.IsEnabled = false;
                toggle.IsChecked = true;
            }

            if (toggle.IsChecked == true)
            {
                // enable realsense
                try
                {
                    // Create an instance of the SenseManager.
                    sm = PXCMSenseManager.CreateInstance();

                    // Enable hand tracking
                    sm.EnableHand();

                    // Get a hand instance here (or inside the AcquireFrame/ReleaseFrame loop) for querying features
                    PXCMHandModule hand = sm.QueryHand();

                    // Initialize the pipeline
                    sm.Init();

                    var handManager = sm.QueryHand();
                    _handConfig = handManager.CreateActiveConfiguration();
                    //_handConfig.EnableGesture("thumb_up");
                    //_handConfig.EnableGesture("thumb_down");
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
                catch (Exception ex)
                {
                    // error handling when SDK and Cameraa don't exist
                    Console.WriteLine(ex);
                    ModernDialog.ShowMessage(BahariModernUI.Resources.StringResources.IntelWarning, BahariModernUI.Resources.StringResources.Warning, MessageBoxButton.OK);
                    toggleContent = BahariModernUI.Resources.StringResources.On;
                    if (AppearanceManager.Current.ThemeSource == AppearanceManager.DarkThemeSource)
                    {
                        toggle1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/dark.camera on.png"));
                    }
                    else
                    {
                        toggle1.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/camera on.png"));
                    }
                    toggle.ToolTip = BahariModernUI.Resources.StringResources.OnLabel;
                    right.IsEnabled = true;
                    left.IsEnabled = true;
                    toggle.IsChecked = false;
                    return;
                }
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
        private void HandRecognition()
        {
            // Stream data
            while (sm.AcquireFrame(true) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                // retrieve hand tracking results if ready
                PXCMHandModule hand2 = sm.QueryHand();
                if (hand2 != null)
                {
                    try
                    {
                        var handQuery = sm.QueryHand();
                        if (handQuery != null)
                        {
                            var handData = handQuery.CreateOutput(); // Get processing results
                            handData.Update();

                            PXCMHandData.GestureData gestureData;
                            PXCMHandData.IHand iHand = null;

                            //if (handData.IsGestureFired("thumb_down", out gestureData))
                            //{
                            //    Console.WriteLine("bad");
                            //}
                            //else if (handData.IsGestureFired("thumb_up", out gestureData))
                            //{
                            //    Console.WriteLine("good");
                            //}
                            //else
                            if (handData.IsGestureFired("spreadfingers", out gestureData))
                            {
                                if (handData.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_RIGHT_HANDS, 0, out iHand) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                                {
                                    //	Position broadcasting
                                    PXCMPointF32 handPosition = iHand.QueryMassCenterImage();
                                    Application.Current.Dispatcher.Invoke(new Action(() =>
                                    {
                                        status = 2;
                                    }));
                                }

                                if (handData.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_LEFT_HANDS, 0, out iHand) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                                {
                                    //	Position broadcasting
                                    PXCMPointF32 handPosition = iHand.QueryMassCenterImage();
                                    Application.Current.Dispatcher.Invoke(new Action(() =>
                                    {
                                        status = 1;
                                    }));
                                }

                            }
                            else if (handData.IsGestureFired("v_sign", out gestureData))
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    status = 0;
                                }));
                            }
                            handData.Dispose();
                        }
                    }
                    finally
                    {
                        // resume next frame processing
                        sm.ReleaseFrame();
                    }
                }
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

        static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
