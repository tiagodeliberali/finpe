using Finpe.CashFlow;

namespace Finpe.MultilineCashflow
{
    public class MultilineDetailTransactionLine : ClassifiedTransactionLine
    {
        public virtual MultiCategoryTransactionLine Parent { get; set; }

        protected MultilineDetailTransactionLine() : base()
        {
        }

        public MultilineDetailTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        public MultilineDetailTransactionLine(TransactionLineInfo info, ClassificationInfo classification) : base(info, classification)
        {
        }
    }
}
