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
    public class MajorTickConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(p => p== null|| p == DependencyProperty.UnsetValue) || double.Parse(values[1].ToString()) == 0)
                return Binding.DoNothing;

            var actualWidth = double.Parse(values[1].ToString());

            var FitCount = (int)(actualWidth / 15);
            
            var majorTicks = (values[0] as List<TickModel>).Where(p => p.IsMajorTick);

            var majorTickCount = majorTicks.Count();
            var Skip = (int)Math.Round((double)majorTickCount / FitCount);


            var StartIndex = 0;

            var tickLabels = majorTicks.Select((p, I) => new { ID = I + StartIndex, Model = p, Index = I });

            var filteredLabels = Skip != 0 ? tickLabels.Where(p => p.ID % Skip == 0) : tickLabels;

            return filteredLabels;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
