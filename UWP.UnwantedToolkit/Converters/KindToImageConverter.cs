using System;
using UWP.UnwantedToolkit.Enums;
using Windows.UI.Xaml.Data;

namespace UWP.UnwantedToolkit.Converters
{
    /// <summary>
    /// Converts an Device Type to Proper Segoe MDL2 Asset Font Icon
    /// </summary>
    public sealed class KindToImageConverter : IValueConverter
    {
        /// <summary>
        /// Convert from DeviceType to Segoe Icon
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string finalvalue = string.Empty;
            switch ((DeviceType)Enum.Parse(typeof(DeviceType), value.ToString()))
            {
                case DeviceType.Desktop:
                    finalvalue = "\xE770";
                    break;
                case DeviceType.Phone:
                case DeviceType.Unknown:
                    finalvalue = "\xE8EA";
                    break;
                case DeviceType.Xbox:
                    finalvalue = "\xE990";
                    break;
                case DeviceType.Tablet:
                    finalvalue = "\xE70A";
                    break;
                case DeviceType.Laptop:
                    finalvalue = "\xE7F8";
                    break;
                default:
                    finalvalue = "\xE770";
                    break;
            }
            return finalvalue;
        }

        /// <summary>
        /// Convert from Icon to DeviceType
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
