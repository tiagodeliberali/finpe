using Finpe.CashFlow;
using Finpe.RecurringCashFlow;
using Finpe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Finpe.Test
{
    public class RecurringTransactionTest
    {
        [Fact]
        public void CreateRecurringTransactions()
        {
            List<TransactionLine> lines = TransactionLineBuilder.BuildList()
                .Add(TransactionLineBuilder.DefaultAmount, month: 4)
                .Build();

            RecurringTransaction transaction = TransactionLineBuilder.BuildRecurringTransaction(TransactionLineBuilder.DefaultAmount);

            transaction.IncludeLines(lines, new YearMonth(2019, 4), new YearMonth(2019, 6));

            Assert.Equal(4, lines.Count);

            RecurringTransactionLine line = GetFist<RecurringTransactionLine>(lines);
            TransactionLineBuilder.ValidateTransactionLine(line);
        }

        [Fact]
        public void CreateRecurringTransactionsConsideringExecutedRecurringTransactions()
        {
            List<TransactionLine> lines = TransactionLineBuilder.BuildList()
                .Add(TransactionLineBuilder.BuildExecutedRecurringTransactionLine(TransactionLineBuilder.DefaultAmount, month: 4))
                .Add(TransactionLineBuilder.BuildExecutedRecurringTransactionLine(TransactionLineBuilder.DefaultAmount, month: 5))
                .Add(TransactionLineBuilder.DefaultAmount, month: 6)
                .Add(TransactionLineBuilder.DefaultAmount, month: 7)
                .Build();

            RecurringTransaction transaction = TransactionLineBuilder.BuildRecurringTransaction(TransactionLineBuilder.DefaultAmount);

            transaction.IncludeLines(lines, new YearMonth(2019, 4), new YearMonth(2019, 7));

            Assert.Equal(6, lines.Count);

            RecurringTransactionLine line = GetFist<RecurringTransactionLine>(lines);
            TransactionLineBuilder.ValidateTransactionLine(line, date: new DateTime(2019, 6, TransactionLineBuilder.DefaultDay));
        }

        [Fact]
        public void CreateRecurringTransactionsRespectingStartDate()
        {
            List<TransactionLine> lines = new List<TransactionLine>();
            RecurringTransaction transaction = TransactionLineBuilder.BuildRecurringTransaction(TransactionLineBuilder.DefaultAmount);
            transaction.StartYearMonth = new YearMonth(2019, 4);

            transaction.IncludeLines(lines, new YearMonth(2019, 1), new YearMonth(2019, 6));

            Assert.Equal(3, lines.Count);
        }

        [Fact]
        public void CreateRecurringTransactionsRespectingEndDate()
        {
            List<TransactionLine> lines = new List<TransactionLine>();
            RecurringTransaction transaction = TransactionLineBuilder.BuildRecurringTransaction(TransactionLineBuilder.DefaultAmount);
            transaction.EndYearMonth = new YearMonth(2019, 2);

            transaction.IncludeLines(lines, new YearMonth(2019, 1), new YearMonth(2019, 6));

            Assert.Equal(2, lines.Count);
        }

        [Fact]
        public void CreateRecurringTransactionsRespectingRangeDate()
        {
            List<TransactionLine> lines = new List<TransactionLine>();
            RecurringTransaction transaction = TransactionLineBuilder.BuildRecurringTransaction(TransactionLineBuilder.DefaultAmount);
            transaction.StartYearMonth = new YearMonth(2019, 2);
            transaction.EndYearMonth = new YearMonth(2019, 4);

            transaction.IncludeLines(lines, new YearMonth(2019, 1), new YearMonth(2019, 6));

            Assert.Equal(3, lines.Count);
        }

        private T GetFist<T>(List<TransactionLine> lines) where T : TransactionLine
        {
            return (T)lines
                .Where(x => x is T)
                .OrderBy(x => x.TransactionDate)
                .FirstOrDefault();
        }
    }
}
