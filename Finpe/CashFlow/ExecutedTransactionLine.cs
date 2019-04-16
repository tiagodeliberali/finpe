namespace Finpe.CashFlow
{
    public class ExecutedTransactionLine : ClassifiedTransactionLine
    {
        public ExecutedTransactionLine(TransactionLineInfo info) : base(info)
        {
            Difference = 0m;
        }

        public ExecutedTransactionLine(TransactionLineInfo info, ClassificationInfo classification) : base(info, classification)
        {
            Difference = 0m;
        }

        public ExecutedTransactionLine(SingleTransactionLine singleTransactionLine, ExecutedTransactionLine statementLine)
            : base(new TransactionLineInfo(statementLine.TransactionDate, statementLine.Amount, singleTransactionLine.Description))
        {
            Difference = singleTransactionLine.Amount - statementLine.Amount;
            Classify(singleTransactionLine.GetClassification());
        }

        public decimal Difference { get; private set; }
    }
}
