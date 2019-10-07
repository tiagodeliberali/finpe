using Finpe.CashFlow;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.MultilineCashflow
{
    public abstract class MultiCategoryTransactionLine : TransactionLine
    {
        protected IList<MultilineDetailTransactionLine> _lines = new List<MultilineDetailTransactionLine>();

        public abstract void Remove(MultilineDetailTransactionLine transactionLine);

        public virtual IReadOnlyCollection<MultilineDetailTransactionLine> Lines => _lines.ToList();

        public MultiCategoryTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        protected MultiCategoryTransactionLine() : base()
        {
        }
    }
}
