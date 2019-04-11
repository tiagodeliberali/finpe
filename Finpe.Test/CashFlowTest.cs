using Finpe.CashFlow;
using Finpe.Statement;
using System;
using Xunit;

namespace Finpe.Test
{
    public class CashFlowTest
    {
        private const string DESCRIPTION = "description";
        private const decimal AMOUNT = 1_100m;
        private DateTime DATE = new DateTime(2019, 4, 10);

        [Fact]
        public void CreateRealizedTransactinLineFromStatementLine()
        {
            StatementLine statementLine = new IncomeStatementLine(DESCRIPTION, AMOUNT, DATE);

            TransactionLine line = new RealizedTransactionLine(statementLine);

            Assert.IsType<RealizedTransactionLine>(line);
            Assert.Equal(DESCRIPTION, line.Description);
            Assert.Equal(AMOUNT, line.Amount);
            Assert.Equal(DATE, line.TransactionDate);
            Assert.Equal(Importance.NotDefined, line.Importance);
            Assert.Equal("", line.Responsible);
            Assert.Equal("", line.Category);
            Assert.Equal(0m, ((RealizedTransactionLine)line).Difference);
        }
    }
}
