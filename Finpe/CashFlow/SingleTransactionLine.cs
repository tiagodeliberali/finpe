namespace Finpe.CashFlow
{
    public class SingleTransactionLine : ClassifiedTransactionLine
    {
        public SingleTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        public ExecutedTransactionLine Consolidate(ExecutedTransactionLine statementLine)
        {
            return new ExecutedTransactionLine(this, statementLine);
        }
    }
}
