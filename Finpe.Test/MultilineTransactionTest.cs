using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using Xunit;

namespace Finpe.Test
{
    public class MultilineTransactionTest
    {
        [Fact]
        public void AddLinesToMultilineTransactionLine()
        {
            MultilineTransactionLine line = TransactionLineBuilder.BuildMultilineTransactionLine();
            line.Add(TransactionLineBuilder.BuildSingleTransactionLine(-300));
            line.Add(TransactionLineBuilder.BuildSingleTransactionLine(-500, "supermercado"));

            Assert.Equal(2, line.Lines.Count);
            Assert.Equal(-800m, line.Amount);
        }

        [Fact]
        public void ConsolidateMultilineTransactionLine()
        {
            MultilineTransactionLine line = TransactionLineBuilder.BuildMultilineTransactionLine();
            line.Add(TransactionLineBuilder.BuildSingleTransactionLine(-300));
            line.Add(TransactionLineBuilder.BuildSingleTransactionLine(-500, "supermercado"));

            ExecutedTransactionLine statementLine = TransactionLineBuilder.BuildExecutedCreditcardTransactionLine(-1_000m);

            ExecutedMultilineTransactionLine executedLine = line.Consolidate(statementLine);

            Assert.Equal(2, executedLine.Lines.Count);

            Assert.Equal(statementLine.Amount, executedLine.Amount);
            Assert.Equal(statementLine.TransactionDate, executedLine.TransactionDate);

            Assert.Equal(line.Description, executedLine.Description);
            Assert.Equal(200m, executedLine.Difference);
        }
    }
}
