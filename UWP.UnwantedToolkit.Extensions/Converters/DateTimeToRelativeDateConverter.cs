using System;
using Windows.UI.Xaml.Data;

namespace UWP.UnwantedToolkit.Extensions.Converters
{
    public class DateTimeToRelativeDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Common.GetRelativeDate(DateTime.Parse(value.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
