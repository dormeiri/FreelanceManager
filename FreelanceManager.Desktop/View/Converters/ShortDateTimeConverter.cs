using System;
using System.Globalization;
using System.Windows.Data;

namespace FreelanceManager.Desktop.View.Converters
{
    public class ShortDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is DateTime dt 
                ? dt.ToString("g") 
                : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string s
                ? DateTime.Parse("g", culture)
                : (DateTime?)null;
        }
    }
}
