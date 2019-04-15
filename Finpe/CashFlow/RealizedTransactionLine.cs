namespace Finpe.CashFlow
{
    public class RealizedTransactionLine : ClassifiedTransactionLine
    {
        public RealizedTransactionLine(StatementTransactionLine statementLine)
            : base(statementLine.TransactionDate, statementLine.Description, statementLine.Amount)
        {
            Difference = 0m;
        }

        public RealizedTransactionLine(SingleTransactionLine singleTransactionLine, StatementTransactionLine statementLine)
            : base(statementLine.TransactionDate, singleTransactionLine.Description, statementLine.Amount)
        {
            Difference = singleTransactionLine.Amount - statementLine.Amount;
            Classify(singleTransactionLine.Category, singleTransactionLine.Responsible, singleTransactionLine.Importance);
        }

        public decimal Difference { get; private set; }
    }
}
