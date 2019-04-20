using Finpe.CashFlow;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.MultilineCashflow
{
    public abstract class MultiCategoryTransactionLine : TransactionLine
    {
        protected IList<SingleTransactionLine> lines = new List<SingleTransactionLine>();

        public virtual IReadOnlyCollection<SingleTransactionLine> Lines => lines.ToList();

        public MultiCategoryTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        protected MultiCategoryTransactionLine() : base()
        {
        }
    }
}
