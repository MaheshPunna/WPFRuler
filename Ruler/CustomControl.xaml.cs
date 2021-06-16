using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class TickModel
    {
        public double Position { get; set; }

        public bool IsMajorTick { get; set; }
    }
    
    public partial class CustomControl : UserControl,INotifyPropertyChanged
    {
        private IEnumerable<TickModel> _tickCollection;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public double UnitCMSize
        {
            get { return (double)GetValue(UnitCMSizeProperty); }
            set { SetValue(UnitCMSizeProperty, value); }
        }

        public static readonly DependencyProperty UnitCMSizeProperty =
            DependencyProperty.Register("UnitCMSize", typeof(double), typeof(CustomControl),new PropertyMetadata(DPPropertyChanged));

        private static void DPPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as CustomControl).UpdateTickCollection();        

        public double ControlWidth
        {
            get { return (double)GetValue(ControlWidthProperty); }
            set { SetValue(ControlWidthProperty, value); }
        }

        public static readonly DependencyProperty ControlWidthProperty =
            DependencyProperty.Register("ControlWidth", typeof(double), typeof(CustomControl),new PropertyMetadata(DPPropertyChanged));

        public double ControlHeight
        {
            get { return (double)GetValue(ControlHeightProperty); }
            set { SetValue(ControlHeightProperty, value); }
        }

        public static readonly DependencyProperty ControlHeightProperty =
            DependencyProperty.Register("ControlHeight", typeof(double), typeof(CustomControl));


        public int MajorCount
        {
            get { return (int)GetValue(MajorCountProperty); }
            set { SetValue(MajorCountProperty, value); }
        }

        public static readonly DependencyProperty MajorCountProperty =
            DependencyProperty.Register("MajorCount", typeof(int), typeof(CustomControl),new PropertyMetadata(DPPropertyChanged));

        public int MinorCount
        {
            get { return (int)GetValue(MinorCountProperty); }
            set { SetValue(MinorCountProperty, value); }
        }

        public static readonly DependencyProperty MinorCountProperty =
            DependencyProperty.Register("MinorCount", typeof(int), typeof(CustomControl),new PropertyMetadata(DPPropertyChanged));



        public bool IsHorizontal
        {
            get { return (bool)GetValue(IsHorizontalProperty); }
            set { SetValue(IsHorizontalProperty, value); }
        }

        public static readonly DependencyProperty IsHorizontalProperty =
            DependencyProperty.Register("IsHorizontal", typeof(bool), typeof(CustomControl), new PropertyMetadata(DPPropertyChanged));

        public IEnumerable<TickModel> TickCollection
        {
            get { return _tickCollection; }
            set
            {
                _tickCollection = value;
                OnPropertyChanged(nameof(TickCollection));
            }
        }


        public CustomControl()
        {
            InitializeComponent();
        }

        public void UpdateTickCollection()
        {
            var _majorTicks = Enumerable.Range(0, MajorCount).Select((p, Index) => new TickModel() { IsMajorTick = true, Position = Index * UnitCMSize });
                

            var DecimalUnitCMSize = UnitCMSize / MinorCount;
            var _minorTicks = _majorTicks.SelectMany(tick => Enumerable.Range(0, MinorCount).Select((p, Index) => new TickModel() { IsMajorTick = false, Position = tick.Position + Index * DecimalUnitCMSize }));

            _majorTicks = _majorTicks.Append(new TickModel() { IsMajorTick = true, Position = MajorCount * UnitCMSize });

            TickCollection = new List<TickModel>(_majorTicks.Concat(_minorTicks));
        }
    }
}
