using System;

namespace Finpe.Utils
{
    public class YearMonth: IComparable<YearMonth>
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
            DateTime date = new DateTime(Year, Month, ValidDay(day));

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return date.AddDays(2);
                case DayOfWeek.Sunday:
                    return date.AddDays(1);
                default:
                    return date;
            }
        }

        private int ValidDay(int day)
        {
            switch (Month)
            {
                case 2:
                    return Math.Min(day, 28);
                case 4:
                    return Math.Min(day, 30);
                case 6:
                    return Math.Min(day, 30);
                case 9:
                    return Math.Min(day, 30);
                case 11:
                    return Math.Min(day, 30);
                default:
                    return Math.Min(day, 31);
            }
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

        public int CompareTo(YearMonth other)
        {
            return Year == other.Year
                ? Month - other.Month
                : Year - other.Year;
        }
    }
}
