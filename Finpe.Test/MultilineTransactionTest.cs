using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using System;
using Xunit;

namespace Finpe.Test
{
    public class MultilineTransactionTest
    {
        private TransactionLineInfo multilineInfo = new TransactionLineInfo(new DateTime(2019, 4, 22), 0m, "Itau personalite");

        [Fact]
        public void AddLinesToMultilineTransactionLine()
        {
            MultilineTransactionLine line = new MultilineTransactionLine(multilineInfo);

            line.Add(new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 15), -300, "farmácia")));
            line.Add(new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 15), -500, "supermercado")));

            Assert.Equal(2, line.Lines.Count);
            Assert.Equal(-800m, line.Amount);
        }

        [Fact]
        public void ConsolidateMultilineTransactionLine()
        {
            MultilineTransactionLine line = new MultilineTransactionLine(multilineInfo);

            line.Add(new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 15), -300, "farmácia")));
            line.Add(new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 15), -500, "supermercado")));

            TransactionLineInfo executedInfo = new TransactionLineInfo(new DateTime(2019, 4, 17), -1_000m, "PGTO CARTAO");
            ExecutedTransactionLine statementLine = new ExecutedTransactionLine(executedInfo);

            ExecutedMultilineTransactionLine executedLine = line.Consolidate(statementLine);

            Assert.Equal(2, executedLine.Lines.Count);
            Assert.Equal(executedInfo.Amount, executedLine.Amount);
            Assert.Equal(executedInfo.TransactionDate, executedLine.TransactionDate);
            Assert.Equal(multilineInfo.Description, executedLine.Description);
            Assert.Equal(200m, executedLine.Difference);
        }
    }
}
