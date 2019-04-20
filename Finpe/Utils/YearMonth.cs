using System;

namespace Finpe.Utils
{
    public class YearMonth
    {
        public int Year { get; private set; }
        public int Month { get; private set; }

        public YearMonth(int year, int month)
        {
            Year = year;
            Month = month;
        }

        protected YearMonth()
        {
        }

        public DateTime ToDate(int day)
        {
            return new DateTime(Year, Month, day);
        }

        public bool Equals(DateTime date)
        {
            return date.Year == Year && date.Month == Month;
        }

        public override bool Equals(object obj)
        {
            return obj is YearMonth month &&
                   Year == month.Year &&
                   Month == month.Month;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Year, Month);
        }

        public YearMonth NextMonth()
        {
            return ToFirstDay().AddMonths(1).ToYearMonth();
        }

        public static bool operator <(YearMonth y1, YearMonth y2)
        {
            return y1.ToFirstDay() < y2.ToFirstDay();
        }

        public static bool operator >(YearMonth y1, YearMonth y2)
        {
            return y1.ToFirstDay() > y2.ToFirstDay();
        }

        public static bool operator <=(YearMonth y1, YearMonth y2)
        {
            return y1.ToFirstDay() <= y2.ToFirstDay();
        }

        public static bool operator >=(YearMonth y1, YearMonth y2)
        {
            return y1.ToFirstDay() >= y2.ToFirstDay();
        }

        private DateTime ToFirstDay()
        {
            return ToDate(1);
        }

        public override string ToString()
        {
            return Year + "/" + Month;
        }
    }
}
