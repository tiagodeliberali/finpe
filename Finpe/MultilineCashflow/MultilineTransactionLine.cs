using Finpe.CashFlow;

namespace Finpe.MultilineCashflow
{
    public class MultilineTransactionLine : MultiCategoryTransactionLine
    {
        public MultilineTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        public void Add(ClassifiedTransactionLine transactionLine)
        {
            lines.Add(transactionLine);
            Amount += transactionLine.Amount;
        }

        public ExecutedMultilineTransactionLine Consolidate(ExecutedTransactionLine statementLine)
        {
            return new ExecutedMultilineTransactionLine(this, statementLine);
        }
    }
}
