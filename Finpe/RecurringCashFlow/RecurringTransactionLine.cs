using Finpe.CashFlow;

namespace Finpe.RecurringCashFlow
{
    public class RecurringTransactionLine : ClassifiedTransactionLine
    {
        public RecurringTransactionLine(TransactionLineInfo info, ClassificationInfo classification) 
            : base(info)
        {
            Classify(classification);
        }
    }
}
