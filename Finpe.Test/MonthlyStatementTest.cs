using Finpe.CashFlow;
using Finpe.RecurringCashFlow;
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

        [Fact]
        public void CreateMonthlyStatementWithRecurringInformation()
        {
            List<RecurringTransaction> recurringTransactions = new List<RecurringTransaction>()
            {
                new RecurringTransaction("Conta de Luz", -200m, 15, new ClassificationInfo("Moradia", "Todos", Importance.Essential))
            };

            List<TransactionLine> statements = new List<TransactionLine>()
            {
                BuildRecurringLine("Conta de Luz", -200m, DateTime.Parse("2019-04-15")),
                BuildLine("salary", 1_000m, DateTime.Parse("2019-04-29")),
                BuildLine("aluguel", -800m, DateTime.Parse("2019-04-30")),
                BuildLine("salary", 1_000m, DateTime.Parse("2019-06-07")),
                BuildLine("aluguel", -800m, DateTime.Parse("2019-06-10"))
            };

            List<MonthlyView> months = MonthlyView.Build(100m, statements, recurringTransactions, new YearMonth(2019, 7));

            Assert.Equal(4, months.Count);

            MonthlyView firstMonth = months[0];
            Assert.Equal(new YearMonth(2019, 4), firstMonth.YearMonth);
            Assert.Equal(100m, firstMonth.InitialAmount);
            Assert.Equal(100m, firstMonth.FinalAmount);
            Assert.Equal(3, firstMonth.Lines.Count);

            MonthlyView secondMonth = months[1];
            Assert.Equal(new YearMonth(2019, 5), secondMonth.YearMonth);
            Assert.Equal(100m, secondMonth.InitialAmount);
            Assert.Equal(-100m, secondMonth.FinalAmount);
            Assert.Equal(1, secondMonth.Lines.Count);

            MonthlyView thirdMonth = months[2];
            Assert.Equal(new YearMonth(2019, 6), thirdMonth.YearMonth);
            Assert.Equal(-100m, thirdMonth.InitialAmount);
            Assert.Equal(-100m, thirdMonth.FinalAmount);
            Assert.Equal(3, thirdMonth.Lines.Count);

            MonthlyView forthMonth = months[3];
            Assert.Equal(new YearMonth(2019, 7), forthMonth.YearMonth);
            Assert.Equal(-100m, forthMonth.InitialAmount);
            Assert.Equal(-300m, forthMonth.FinalAmount);
            Assert.Equal(1, forthMonth.Lines.Count);
        }

        private ExecutedTransactionLine BuildLine(string description, decimal amount, DateTime date)
        {
            return new ExecutedTransactionLine(new TransactionLineInfo(date, amount, description));
        }

        private ExecutedRecurringTransactionLine BuildRecurringLine(string description, decimal amount, DateTime date)
        {
            return new ExecutedRecurringTransactionLine(new TransactionLineInfo(date, amount, description));
        }
    }
}
