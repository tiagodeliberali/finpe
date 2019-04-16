using Finpe.CashFlow;
using System.Collections.Generic;

namespace Finpe.MultilineCashflow
{
    public abstract class MultiCategoryTransactionLine : TransactionLine
    {
        protected List<ClassifiedTransactionLine> lines = new List<ClassifiedTransactionLine>();

        public IReadOnlyCollection<ClassifiedTransactionLine> Lines
        {
            get
            {
                return lines;
            }
        }

        public MultiCategoryTransactionLine(TransactionLineInfo info) : base(info)
        {
        }
    }
}
