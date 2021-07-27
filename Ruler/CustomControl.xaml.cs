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
        public bool IsVisible { get; set; }
        public bool IsEndMarker { get; set; }

        public TickModel()
        {
            IsVisible = true;
        }
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
            DependencyProperty.Register("ControlHeight", typeof(double), typeof(CustomControl), new PropertyMetadata(DPPropertyChanged));


        //public double MajorCount
        //{
        //    get { return (double)GetValue(MajorCountProperty); }
        //    set { SetValue(MajorCountProperty, value); }
        //}

        //public static readonly DependencyProperty MajorCountProperty =
        //    DependencyProperty.Register("MajorCount", typeof(double), typeof(CustomControl),new PropertyMetadata(DPPropertyChanged));

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
        public double RotationAngle
        {
            get { return (double)GetValue(RotationAngleProperty); }
            set { SetValue(RotationAngleProperty, value); }
        }

        public static readonly DependencyProperty RotationAngleProperty =
            DependencyProperty.Register("RotationAngle", typeof(double), typeof(CustomControl), new PropertyMetadata(DPPropertyChanged));




        public bool LabelVisible
        {
            get { return (bool)GetValue(LabelVisibleProperty); }
            set { SetValue(LabelVisibleProperty, value); }
        }

        public static readonly DependencyProperty LabelVisibleProperty =
            DependencyProperty.Register("LabelVisible", typeof(bool), typeof(CustomControl),new PropertyMetadata(false));



        public CustomControl()
        {
            InitializeComponent();
        }

        public void UpdateTickCollection()
        {
            var MajorCount = ControlWidth / (UnitCMSize);

            if (double.IsNaN(MajorCount) || double.IsInfinity(MajorCount) || MajorCount == 0)
            {
                TickCollection = new List<TickModel>();
                return;
            }

            var IntMajorCount = (int)MajorCount;
            var Frac_MinorTicks = (MajorCount - IntMajorCount)*MinorCount;

            var _majorTicks = Enumerable.Range(0, IntMajorCount).Select((p, Index) => new TickModel() { IsMajorTick = true, Position = Index * UnitCMSize });


            var DecimalUnitCMSize = UnitCMSize / MinorCount;
            var _minorTicks = _majorTicks.SelectMany(tick => Enumerable.Range(0, MinorCount).Select((p, Index) => new TickModel() { IsMajorTick = false, Position = tick.Position + Index * DecimalUnitCMSize }));

            _majorTicks = _majorTicks.Append(new TickModel() { IsMajorTick = true, Position = IntMajorCount * UnitCMSize });

            var all_ticks = _majorTicks.Concat(_minorTicks);
            if(all_ticks.Count() > 0)
            {
                for (var I = 1; I <= Frac_MinorTicks; I++)                
                    all_ticks = all_ticks.Append(new TickModel() { IsMajorTick = false, Position = _majorTicks.Last().Position + I * DecimalUnitCMSize });                
            }
            TickCollection = new List<TickModel>(all_ticks);

            //var Length = Math.Sqrt(Math.Pow(ControlWidth,2)+ Math.Pow(ControlHeight,2));
            //var MajorCount = Length / (UnitCMSize);

            //if (double.IsNaN(MajorCount)|| double.IsInfinity(MajorCount) || MajorCount == 0)
            //    return;

            //var m_count = (int)(MajorCount - (MajorCount%2));

            //var remaining_fraction= (MajorCount - m_count) / 2.0;

            //var Offset = remaining_fraction * UnitCMSize;

            //var _majorTicks = Enumerable.Range(0, m_count).Select((p, Index) => new TickModel() { IsMajorTick = true, Position = Index * UnitCMSize + Offset });

            //var DecimalUnitCMSize = UnitCMSize / MinorCount;
            //var _minorTicks = _majorTicks.SelectMany(tick => Enumerable.Range(0, MinorCount).Select((p, Index) => new TickModel() { IsMajorTick = false, Position = tick.Position + Index * DecimalUnitCMSize }));


            //if (_minorTicks.Count() > 0)
            //    _majorTicks = _majorTicks.Append(new TickModel() { IsMajorTick = true, Position = _minorTicks.Last().Position + DecimalUnitCMSize });

            //var all_ticks = _majorTicks.Concat(_minorTicks);

            //var _fractional_minor_ticks = (int)(remaining_fraction * MinorCount);
            //if (all_ticks.Count() > 0)
            //{
            //    for (var I = 1; I <= _fractional_minor_ticks; I++)
            //    {
            //        all_ticks = all_ticks.Prepend(new TickModel() { IsMajorTick = false, Position = _majorTicks.First().Position - I*DecimalUnitCMSize });
            //        all_ticks = all_ticks.Append(new TickModel() { IsMajorTick = false, Position = _majorTicks.Last().Position + I*DecimalUnitCMSize });
            //    }
            //}
            //all_ticks = all_ticks.Prepend(new TickModel() { IsEndMarker = true,IsVisible=false, Position = 0 });
            //all_ticks = all_ticks.Append(new TickModel() { IsEndMarker = true,IsVisible=false, Position = UnitCMSize * MajorCount });

            //TickCollection = new List<TickModel>(all_ticks);
        }
    }
}
