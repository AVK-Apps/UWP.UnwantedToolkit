namespace UWP.UnwantedToolkit.Extensions
{
    public static class StringExtension
    {
        public static string AsNumeriK(this string str)
        {
            if (double.TryParse(str.ToString(), out double result))
            {
                return Common.NumeriK(result);
            }
            return str.ToString();
        }
    }
}
