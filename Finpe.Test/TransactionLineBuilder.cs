using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using Finpe.RecurringCashFlow;
using System;
using System.Collections.Generic;
using Xunit;

namespace Finpe.Test
{
    public class TransactionLineBuilder
    {
        public static readonly string DefaultCategory = "Moradia";
        public static readonly string DefaultDescription = "farmácia";
        public static readonly decimal DefaultAmount = 1_100m;
        public static readonly DateTime DefaultDate = new DateTime(2019, 4, 10);
        public static readonly int DefaultDay = 10;
        public static readonly string DefaultResponsible = "Tiago";
        public static readonly Importance DefaultImportance = Importance.Essential;

        public static TransactionLineInfo BuildDefaultTransactionInfo()
        {
            return new TransactionLineInfo(DefaultDate, DefaultAmount, DefaultDescription);
        }

        public static ClassificationInfo BuildDefaultClssification()
        {
            return new ClassificationInfo(DefaultCategory, DefaultResponsible, DefaultImportance);
        }

        public static SingleTransactionLine BuildSingleTransactionLine(decimal amount, string description = "farmácia", int day = 10, int month = 4, string category = "Moradia")
        {
            return new SingleTransactionLine(
                new TransactionLineInfo(new DateTime(2019, month, day), amount, description),
                new ClassificationInfo(category, DefaultResponsible, DefaultImportance));
        }

        public static ExecutedRecurringTransactionLine BuildExecutedRecurringTransactionLine(decimal amount, string description = "farmácia", int day = 10, int month = 4)
        {
            return new ExecutedRecurringTransactionLine(new TransactionLineInfo(new DateTime(2019, month, day), amount, description));
        }

        public static RecurringTransaction BuildRecurringTransaction(decimal amount, string description = "farmácia", int day = 10)
        {
            return new RecurringTransaction(description, amount, day, BuildDefaultClssification());
        }

        public static MultilineTransactionLine BuildMultilineTransactionLine()
        {
            return new MultilineTransactionLine(new TransactionLineInfo(DefaultDate, 0m, "Itau personalite"));
        }

        public static ExecutedTransactionLine BuildExecutedCreditcardTransactionLine(decimal amount)
        {
            return new ExecutedTransactionLine(new TransactionLineInfo(DefaultDate, amount, "PGTO CARTAO"));
        }

        public static TransactionListBuilder BuildList()
        {
            return new TransactionListBuilder();
        }

        public static void ValidateTransactionLine(ClassifiedTransactionLine line, string description = "farmácia", Importance importance = Importance.Essential, string responsible = "Tiago", string category = "Moradia", DateTime? date = null)
        {
            Assert.Equal(description, line.Description);
            Assert.Equal(DefaultAmount, line.Amount);
            Assert.Equal(date.HasValue ? date.Value : DefaultDate, line.TransactionDate);
            Assert.Equal(importance, line.Importance);
            Assert.Equal(responsible, line.Responsible);
            Assert.Equal(category, line.Category);
        }
    }

    public class TransactionListBuilder
    {
        List<TransactionLine> transactionList = new List<TransactionLine>();

        internal TransactionListBuilder()
        {
        }

        public TransactionListBuilder Add(decimal amount, string description = "farmácia", int day = 10, int month = 4, string category = "Moradia")
        {
            transactionList.Add(TransactionLineBuilder.BuildSingleTransactionLine(amount, description, day, month, category));
            return this;
        }

        public TransactionListBuilder Add(TransactionLine line)
        {
            transactionList.Add(line);
            return this;
        }

        public List<TransactionLine> Build()
        {
            return transactionList;
        }
    }
}
