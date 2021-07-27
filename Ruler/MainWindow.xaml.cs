using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Ruler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool _IsDrawing;
        Point StartPoint;
        public MainWindow()
        {
            InitializeComponent();

            this.MouseDown += (s, e) =>
            {
                if (!_IsDrawing)
                {
                    StartPoint = Mouse.GetPosition(ParentCanvas);

                    Canvas.SetLeft(H1, StartPoint.X);
                    Canvas.SetTop(H1, StartPoint.Y - H1.ActualHeight / 2);

                    H1.ControlWidth = 0;
                }
                _IsDrawing = !_IsDrawing;
            };
            this.MouseMove += (s, e) => {

                if (_IsDrawing)
                {
                    var MousePoint = Mouse.GetPosition(ParentCanvas);

                    H1.ControlWidth = Point.Subtract(MousePoint,StartPoint).Length;

                    H1.RotationAngle = -1* Math.Atan2(MousePoint.Y - StartPoint.Y, MousePoint.X - StartPoint.X) * 180.0 / Math.PI;
                }
            };
        }
    }
}
