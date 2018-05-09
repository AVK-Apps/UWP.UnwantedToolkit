using System;
using Windows.UI.Xaml.Data;

namespace UWP.UnwantedToolkit.Extensions.Converters
{
    public class DateTimeToRelativeDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return DateTime.Parse(value.ToString()).AsRelativeDate();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
