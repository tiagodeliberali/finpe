using Finpe.Statement;

namespace Finpe.CashFlow
{
    public class RealizedTransactionLine : TransactionLine
    {
        public RealizedTransactionLine(StatementLine statementLine)
            : base(statementLine.TransactionDate, statementLine.Description, statementLine.CalculateNewAmount(0))
        {
            Difference = 0m;
            StatementLine = statementLine;
        }

        public RealizedTransactionLine(SingleTransactionLine singleTransactionLine, StatementLine statementLine)
            : base(statementLine.TransactionDate, singleTransactionLine.Description, statementLine.Amount)
        {
            Difference = singleTransactionLine.Amount - statementLine.Amount;
            StatementLine = statementLine;
            Classify(singleTransactionLine.Category, singleTransactionLine.Responsible, singleTransactionLine.Importance);
        }

        public decimal Difference { get; private set; }
        public StatementLine StatementLine { get; set; }
    }
}
