using System;

namespace Finpe.Utils
{
    public static class DateTimeExtensions
    {
        public static YearMonth ToYearMonth(this DateTime date)
        {
            return new YearMonth(date.Year, date.Month);
        }
    }
}
