using Finpe.Budget;
using Finpe.CashFlow;
using FluentNHibernate.Mapping;

namespace Finpe.Api.Budget
{
    public class BudgetTransactionLineMap : SubclassMap<BudgetTransactionLine>
    {
        public BudgetTransactionLineMap()
        {
            DiscriminatorValue(TransactionLineTypes.Budget);
        }
    }
}
