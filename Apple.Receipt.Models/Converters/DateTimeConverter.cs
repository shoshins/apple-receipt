using System;

namespace Apple.Receipt.Models.Converters
{
    public static class DateTimeConverter
    {
        public static DateTime? MillisecondsToDate(string? millisecondsString)
        {
            if (string.IsNullOrEmpty(millisecondsString))
            {
                return null;
            }

            if (!long.TryParse(millisecondsString, out var milliseconds))
            {
                return null;
            }

            var dt = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).UtcDateTime;
            return dt;
        }
    }
}
