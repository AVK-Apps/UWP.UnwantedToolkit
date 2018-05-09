using System;

namespace UWP.UnwantedToolkit.Extensions
{
    public static class DateTimeExtension
    {
        public static string AsRelativeDate(this DateTime value)
        {
            return Common.GetRelativeDate(DateTime.Parse(value.ToString()));
        }
    }
}
