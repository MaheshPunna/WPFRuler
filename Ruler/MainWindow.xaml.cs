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

namespace Ruler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += (s, e) => {

                foreach (FrameworkElement Child in ParentCanvas.Children)
                {
                    Canvas.SetLeft(Child, ParentCanvas.Width / 2 - Child.ActualWidth / 2);
                    Canvas.SetTop(Child, ParentCanvas.Height / 2 - Child.ActualHeight / 2);
                }
            };
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Slider.Value = double.Parse((sender as TextBox).Text);
            }
            catch (Exception ex) { }
        }
    }
}
