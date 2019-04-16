using Finpe.CashFlow;
using System;
using Xunit;

namespace Finpe.Test
{
    public class CashFlowTest
    {
        [Fact]
        public void ClassifySingleTransaction()
        {
            SingleTransactionLine line = new SingleTransactionLine(TransactionLineBuilder.BuildDefaultTransactionInfo());
            line.Classify(TransactionLineBuilder.BuildDefaultClssification());

            TransactionLineBuilder.ValidateTransactionLine(line);
        }

        [Fact]
        public void CreateExecutedTransactionLine()
        {
            ExecutedTransactionLine line = new ExecutedTransactionLine(TransactionLineBuilder.BuildDefaultTransactionInfo());

            TransactionLineBuilder.ValidateTransactionLine(line, importance: Importance.NotDefined, responsible: "", category: "");
            Assert.Equal(0m, ((ExecutedTransactionLine)line).Difference);
        }

        [Fact]
        public void CreateRealizedTransactionLineFromSingleTransaction()
        {
            ExecutedTransactionLine statementLine = new ExecutedTransactionLine(TransactionLineBuilder.BuildDefaultTransactionInfo());

            TransactionLineInfo lineInfo = new TransactionLineInfo(new DateTime(2019, 4, 12), 2_000m, "transaction description");
            SingleTransactionLine singleTransactionLine = new SingleTransactionLine(lineInfo);
            singleTransactionLine.Classify(TransactionLineBuilder.BuildDefaultClssification());

            ExecutedTransactionLine line = singleTransactionLine.Consolidate(statementLine);

            TransactionLineBuilder.ValidateTransactionLine(line, description: lineInfo.Description);
            Assert.Equal(900m, line.Difference);
        }
    }
}
