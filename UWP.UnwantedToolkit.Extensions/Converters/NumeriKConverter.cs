using System;
using Windows.UI.Xaml.Data;

namespace UWP.UnwantedToolkit.Extensions.Converters
{
    public class NumeriKConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (double.TryParse(value.ToString(), out double result))
            {
                return Common.NumeriK(result);
            }
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
