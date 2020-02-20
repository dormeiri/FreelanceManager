using System;
using System.Globalization;
using System.Windows.Data;

namespace FreelanceManager.Desktop.View.Converters
{
    public class IsSelectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is int selectedIndex && selectedIndex >= 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool isSelected && isSelected;
        }
    }
}
