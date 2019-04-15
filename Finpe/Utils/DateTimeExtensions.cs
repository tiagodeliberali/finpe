using System;

namespace Finpe.Utils
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDay(this DateTime date)
        {
            return FirstDay(date).AddMonths(1).AddDays(-1);
        }

        public static YearMonth ToYearMonth(this DateTime date)
        {
            return new YearMonth(date.Year, date.Month);
        }
    }
}
