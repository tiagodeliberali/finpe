using Finpe.CashFlow;

namespace Finpe.MultilineCashflow
{
    public class MultilineTransactionLine : MultiCategoryTransactionLine
    {
        public MultilineTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        protected MultilineTransactionLine() : base()
        {
        }

        public virtual void Add(SingleTransactionLine transactionLine)
        {
            lines.Add(transactionLine);
            Amount += transactionLine.Amount;
        }

        public virtual ExecutedMultilineTransactionLine Consolidate(ExecutedTransactionLine statementLine)
        {
            return new ExecutedMultilineTransactionLine(this, statementLine);
        }
    }
}
