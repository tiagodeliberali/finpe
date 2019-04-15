using Finpe.CashFlow;
using Finpe.Utils;
using Finpe.Visualization;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Finpe.Test
{
    public class MonthlyStatementTest
    {
        [Fact]
        public void CreateMonthlyStatement()
        {
            List<TransactionLine> statements = new List<TransactionLine>()
            {
                BuildLine("salary", 1_000m, DateTime.Parse("2019-04-07")),
                BuildLine("aluguel", -800m, DateTime.Parse("2019-04-10"))
            };

            List<MonthlyView> months = MonthlyView.Build(100m, statements);

            Assert.Single(months);
            MonthlyView month = months.First();
            Assert.Equal(new YearMonth(2019, 4), month.YearMonth);
            Assert.Equal(100m, month.InitialAmount);
            Assert.Equal(300m, month.FinalAmount);
            Assert.Equal(2, month.Lines.Count);
        }

        [Fact]
        public void CreateMonthlyStatementForMoreMonths()
        {
            List<TransactionLine> statements = new List<TransactionLine>()
            {
                BuildLine("salary", 1_000m, DateTime.Parse("2019-04-07")),
                BuildLine("aluguel", -800m, DateTime.Parse("2019-04-10")),
                BuildLine("salary", 1_000m, DateTime.Parse("2019-05-07")),
                BuildLine("aluguel", -800m, DateTime.Parse("2019-05-10")),
                BuildLine("salary", 1_000m, DateTime.Parse("2019-06-07")),
                BuildLine("aluguel", -800m, DateTime.Parse("2019-06-10"))
            };

            List<MonthlyView> months = MonthlyView.Build(100m, statements);

            Assert.Equal(3, months.Count);

            MonthlyView firstMonth = months[0];
            Assert.Equal(new YearMonth(2019, 4), firstMonth.YearMonth);
            Assert.Equal(100m, firstMonth.InitialAmount);
            Assert.Equal(300m, firstMonth.FinalAmount);
            Assert.Equal(2, firstMonth.Lines.Count);

            MonthlyView secondMonth = months[1];
            Assert.Equal(new YearMonth(2019, 5), secondMonth.YearMonth);
            Assert.Equal(300m, secondMonth.InitialAmount);
            Assert.Equal(500m, secondMonth.FinalAmount);
            Assert.Equal(2, secondMonth.Lines.Count);

            MonthlyView thirdMonth = months[2];
            Assert.Equal(new YearMonth(2019, 6), thirdMonth.YearMonth);
            Assert.Equal(500m, thirdMonth.InitialAmount);
            Assert.Equal(700m, thirdMonth.FinalAmount);
            Assert.Equal(2, thirdMonth.Lines.Count);
        }

        [Fact]
        public void CreateMonthlyStatementForMoreMonthsFillingEmptyMonths()
        {
            List<TransactionLine> statements = new List<TransactionLine>()
            {
                BuildLine("salary", 1_000m, DateTime.Parse("2019-04-29")),
                BuildLine("aluguel", -800m, DateTime.Parse("2019-04-30")),
                BuildLine("salary", 1_000m, DateTime.Parse("2019-06-07")),
                BuildLine("aluguel", -800m, DateTime.Parse("2019-06-10"))
            };

            List<MonthlyView> months = MonthlyView.Build(100m, statements);

            Assert.Equal(3, months.Count);

            MonthlyView firstMonth = months[0];
            Assert.Equal(new YearMonth(2019, 4), firstMonth.YearMonth);
            Assert.Equal(100m, firstMonth.InitialAmount);
            Assert.Equal(300m, firstMonth.FinalAmount);
            Assert.Equal(2, firstMonth.Lines.Count);

            MonthlyView secondMonth = months[1];
            Assert.Equal(new YearMonth(2019, 5), secondMonth.YearMonth);
            Assert.Equal(300m, secondMonth.InitialAmount);
            Assert.Equal(300m, secondMonth.FinalAmount);
            Assert.Equal(0, secondMonth.Lines.Count);

            MonthlyView thirdMonth = months[2];
            Assert.Equal(new YearMonth(2019, 6), thirdMonth.YearMonth);
            Assert.Equal(300m, thirdMonth.InitialAmount);
            Assert.Equal(500m, thirdMonth.FinalAmount);
            Assert.Equal(2, thirdMonth.Lines.Count);
        }

        private ExecutedTransactionLine BuildLine(string description, decimal amount, DateTime date)
        {
            return new ExecutedTransactionLine(date, description, amount);
        }
    }
}
