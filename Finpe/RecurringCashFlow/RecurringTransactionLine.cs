using Finpe.CashFlow;

namespace Finpe.RecurringCashFlow
{
    public class RecurringTransactionLine : ClassifiedTransactionLine
    {
        public virtual long RecurringTransactionId { get; set; }

        public RecurringTransactionLine(long recurringTransactionId, TransactionLineInfo info, ClassificationInfo classification) 
            : base(info)
        {
            Classify(classification);
            RecurringTransactionId = recurringTransactionId;
        }

        protected RecurringTransactionLine() : base()
        {
        }
    }
}
