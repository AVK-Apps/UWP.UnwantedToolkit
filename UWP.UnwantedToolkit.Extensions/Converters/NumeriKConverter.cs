using System;
using Windows.UI.Xaml.Data;

namespace UWP.UnwantedToolkit.Extensions.Converters
{
    public class NumeriKConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString().AsNumeriK();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
