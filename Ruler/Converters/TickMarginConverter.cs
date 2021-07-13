using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ruler.Converters
{
    public class TickLabelMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Size = double.Parse(value.ToString());
            if (Size == 0)
                return Binding.DoNothing;

            if (parameter == null)
                return new Thickness(-Size / 2, -27, 0, 0);
            else
                return new Thickness(-27, -Size/2, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TickMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Size = double.Parse(value.ToString());
            if (parameter == null)
                return new Thickness(Size, 0, 0, 0);
            else
                return new Thickness(0, Size, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
