﻿using System;
using Windows.UI.Xaml.Data;

namespace UWP.UnwantedToolkit.Converters
{
    public sealed class KindToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string finalvalue = string.Empty;
            switch ((string)value)
            {
                case "Desktop":
                    finalvalue = "\xE770";
                    break;
                case "Phone":
                case "Unknown":
                    finalvalue = "\xE8EA";
                    break;
                case "Xbox":
                    finalvalue = "\xE990";
                    break;
                case "Tablet":
                    finalvalue = "\xE70A";
                    break;
                case "Laptop":
                    finalvalue = "\xE7F8";
                    break;
                default:
                    finalvalue = "\xE770";
                    break;
            }
            return finalvalue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
