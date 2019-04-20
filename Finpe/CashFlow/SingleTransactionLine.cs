namespace Finpe.CashFlow
{
    public class SingleTransactionLine : ClassifiedTransactionLine
    {
        public SingleTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        public SingleTransactionLine(TransactionLineInfo info, ClassificationInfo classification) : base(info, classification)
        {
        }

        protected SingleTransactionLine() : base()
        {
        }

        public virtual ExecutedTransactionLine Consolidate(ExecutedTransactionLine statementLine)
        {
            return new ExecutedTransactionLine(this, statementLine);
        }
    }
}
