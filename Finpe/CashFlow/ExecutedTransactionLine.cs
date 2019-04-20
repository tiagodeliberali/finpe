namespace Finpe.CashFlow
{
    public class ExecutedTransactionLine : ClassifiedTransactionLine
    {
        public virtual decimal Difference { get; protected set; }

        public ExecutedTransactionLine(TransactionLineInfo info) : base(info)
        {
            Difference = 0m;
        }

        public ExecutedTransactionLine(TransactionLineInfo info, ClassificationInfo classification) : base(info, classification)
        {
            Difference = 0m;
        }

        protected ExecutedTransactionLine() : base()
        {
            Difference = 0m;
        }

        public ExecutedTransactionLine(SingleTransactionLine singleTransactionLine, ExecutedTransactionLine statementLine)
            : base(new TransactionLineInfo(statementLine.TransactionDate, statementLine.Amount, singleTransactionLine.Description))
        {
            Difference = singleTransactionLine.Amount - statementLine.Amount;
            Classify(singleTransactionLine.GetClassification());
        }
    }
}
