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
        private const string description = "description";
        private const decimal amount = 1_100m;
        private const int day = 15;
        string category = "Moradia";
        string responsible = "Tiago";
        Importance importance = Importance.Essential;

        [Fact]
        public void CreateRecurringTransactions()
        {
            List<TransactionLine> lines = new List<TransactionLine>()
            {
                new SingleTransactionLine(description, amount, new DateTime(2019, 4, day)),
            };
            RecurringTransaction transaction = new RecurringTransaction(description, amount, day, category, responsible, importance);

            transaction.IncludeLines(lines, new YearMonth(2019, 4), new YearMonth(2019, 12));

            Assert.Equal(10, lines.Count);

            RecurringTransactionLine line = GetFist<RecurringTransactionLine>(lines);
            Assert.Equal(description, line.Description);
            Assert.Equal(amount, line.Amount);
            Assert.Equal(new DateTime(2019, 4, day), line.TransactionDate);
            Assert.Equal(category, line.Category);
            Assert.Equal(responsible, line.Responsible);
            Assert.Equal(importance, line.Importance);
        }

        [Fact]
        public void CreateRecurringTransactionsConsideringExecutedRecurringTransactions()
        {
            List<TransactionLine> lines = new List<TransactionLine>()
            {
                new ExecutedRecurringTransactionLine(new DateTime(2019, 4, day), description, amount),
                new ExecutedRecurringTransactionLine(new DateTime(2019, 5, day), description, amount),
                new SingleTransactionLine(description, amount, new DateTime(2019, 6, day)),
                new SingleTransactionLine(description, amount, new DateTime(2019, 7, day))
            };
            RecurringTransaction transaction = new RecurringTransaction(description, amount, day, category, responsible, importance);

            transaction.IncludeLines(lines, new YearMonth(2019, 4), new YearMonth(2019, 12));

            Assert.Equal(11, lines.Count);

            RecurringTransactionLine line = GetFist<RecurringTransactionLine>(lines);
            Assert.Equal(description, line.Description);
            Assert.Equal(amount, line.Amount);
            Assert.Equal(new DateTime(2019, 6, day), line.TransactionDate);
            Assert.Equal(category, line.Category);
            Assert.Equal(responsible, line.Responsible);
            Assert.Equal(importance, line.Importance);
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
