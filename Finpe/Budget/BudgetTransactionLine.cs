using Finpe.CashFlow;

namespace Finpe.Budget
{
    public class BudgetTransactionLine : ClassifiedTransactionLine
    {
        public BudgetTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        public BudgetTransactionLine(TransactionLineInfo info, ClassificationInfo classification) : base(info, classification)
        {
        }

        protected BudgetTransactionLine() : base()
        {
        }
    }
}
