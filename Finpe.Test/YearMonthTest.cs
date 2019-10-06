using Finpe.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Finpe.Test
{
    public class YearMonthTest
    {
        [Theory]
        [InlineData(5, 7)]
        [InlineData(13, 14)]
        [InlineData(9, 9)]
        public void ToDate_ShouldConvertToBusinessDay(int originalDay, int businessDay)
        {
            YearMonth yearMonth = new YearMonth(2019, 10);

            DateTime date = yearMonth.ToDate(originalDay);

            Assert.Equal(new DateTime(2019, 10, businessDay), date);
        }

        [Theory]
        [InlineData(2, 31, 28)]
        [InlineData(2, 30, 28)]
        [InlineData(7, 31, 31)]
        [InlineData(9, 31, 30)]
        [InlineData(5, 35, 31)]
        public void ToDate_ShouldConsiderValidLastDay(int month, int originalDay, int businessDay)
        {
            YearMonth yearMonth = new YearMonth(2019, month);

            DateTime date = yearMonth.ToDate(originalDay);

            Assert.Equal(new DateTime(2019, month, businessDay), date);
        }
    }
}
