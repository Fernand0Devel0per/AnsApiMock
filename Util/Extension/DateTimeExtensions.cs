namespace MockAbiANS.Util.Extension
{
    public static class DateTimeExtensions
    {
        public static DateTime ToLocalTimeFromUtc(this DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToLocalTime();
        }
    }
}
