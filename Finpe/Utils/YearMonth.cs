using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
