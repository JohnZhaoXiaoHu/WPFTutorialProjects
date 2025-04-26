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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zhaoxi.SCADA.Demo.ViewModels;

namespace Zhaoxi.SCADA.Demo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
        }

        private void Viewport3D_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var camera = (sender as Viewport3D).Camera as PerspectiveCamera;
            if (e.Delta < 0 && camera.FieldOfView + 2 > 160)
            {
                camera.FieldOfView = 160;
                return;
            }
            if (e.Delta > 0 && camera.FieldOfView - 2 < 110)
            {
                camera.FieldOfView = 110;
                return;
            }
            if (e.Delta > 0)
                camera.FieldOfView -= 2;
            else if (e.Delta < 0)
                camera.FieldOfView += 2;

        }

        private void Viewport3D_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var camera = (sender as Viewport3D).Camera as PerspectiveCamera;
                Point new_point = e.GetPosition((IInputElement)sender);

                camera.Transform = new TranslateTransform3D(
                    start_point.X - new_point.X,
                    new_point.Y - start_point.Y,
                    0);

            }
        }
        Point start_point;
        private void Viewport3D_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var camera = (sender as Viewport3D).Camera as PerspectiveCamera;
            start_point = e.GetPosition((IInputElement)sender);

            if (camera.Transform is TranslateTransform3D)
            {
                TranslateTransform3D tt = camera.Transform as TranslateTransform3D;
                start_point.X += tt.OffsetX;
                start_point.Y -= tt.OffsetY;
            }

            e.MouseDevice.Capture((IInputElement)sender);
        }

        private void Viewport3D_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.MouseDevice.Capture(null);
        }
    }
}
