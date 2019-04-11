using Finpe.CashFlow;
using Finpe.Statement;
using System;
using Xunit;

namespace Finpe.Test
{
    public class CashFlowTest
    {
        private const string description = "description";
        private const decimal amount = 1_100m;
        private DateTime date = new DateTime(2019, 4, 10);
        string category = "Moradia";
        string responsible = "Tiago";
        Importance importance = Importance.Essential;

        [Fact]
        public void ClassifySingleTransaction()
        {
            SingleTransactionLine line = new SingleTransactionLine(description, amount, date);
            line.Classify(category, responsible, importance);

            Assert.Equal(description, line.Description);
            Assert.Equal(amount, line.Amount);
            Assert.Equal(date, line.TransactionDate);
            Assert.Equal(importance, line.Importance);
            Assert.Equal(responsible, line.Responsible);
            Assert.Equal(category, line.Category);
        }

        [Fact]
        public void CreateRealizedTransactionLineFromStatementLine()
        {
            StatementLine statementLine = new IncomeStatementLine(description, amount, date);

            TransactionLine line = new RealizedTransactionLine(statementLine);

            Assert.IsType<RealizedTransactionLine>(line);
            Assert.Equal(description, line.Description);
            Assert.Equal(amount, line.Amount);
            Assert.Equal(date, line.TransactionDate);
            Assert.Equal(Importance.NotDefined, line.Importance);
            Assert.Equal("", line.Responsible);
            Assert.Equal("", line.Category);
            Assert.Equal(0m, ((RealizedTransactionLine)line).Difference);
        }

        [Fact]
        public void CreateRealizedTransactionLineFromSingleTransaction()
        {
            string transactionDescription = "transaction description";
            decimal transactionAmount = 2_000m;
            DateTime transactionDate = new DateTime(2019, 4, 12);

            StatementLine statementLine = new IncomeStatementLine(description, amount, date);

            SingleTransactionLine singleTransactionLine = new SingleTransactionLine(transactionDescription, transactionAmount, transactionDate);
            singleTransactionLine.Classify(category, responsible, importance);

            TransactionLine line = singleTransactionLine.Consolidate(statementLine);

            Assert.IsType<RealizedTransactionLine>(line);
            Assert.Equal(transactionDescription, line.Description);
            Assert.Equal(amount, line.Amount);
            Assert.Equal(date, line.TransactionDate);
            Assert.Equal(importance, line.Importance);
            Assert.Equal(responsible, line.Responsible);
            Assert.Equal(category, line.Category);
            Assert.Equal(900m, ((RealizedTransactionLine)line).Difference);
        }
    }
}
