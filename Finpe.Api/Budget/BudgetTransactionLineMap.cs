using Finpe.Api.Utils;
using Finpe.Budget;
using Finpe.CashFlow;

namespace Finpe.Api.Budget
{
    public class BudgetTransactionLineMap : ClassifiedTransactionLineSubclassMap<BudgetTransactionLine>
    {
        public BudgetTransactionLineMap() : base()
        {
            DiscriminatorValue(TransactionLineTypes.Budget);
        }
    }
}
