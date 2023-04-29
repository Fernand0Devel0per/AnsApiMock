using System.Globalization;

namespace MockAbiANS.Util.Extension
{
    public static class StringExtensions
    {
        public static DateTime ToDateTime(this string str, string format, CultureInfo culture)
        {
            return DateTime.ParseExact(str, format, culture);
        }
    }
}
