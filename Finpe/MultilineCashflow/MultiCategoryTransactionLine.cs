using Finpe.CashFlow;
using System.Collections.Generic;

namespace Finpe.MultilineCashflow
{
    public abstract class MultiCategoryTransactionLine : TransactionLine
    {
        protected List<SingleTransactionLine> lines = new List<SingleTransactionLine>();

        public virtual IReadOnlyCollection<SingleTransactionLine> Lines
        {
            get
            {
                return lines;
            }
        }

        public MultiCategoryTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        protected MultiCategoryTransactionLine() : base()
        {
        }
    }
}
