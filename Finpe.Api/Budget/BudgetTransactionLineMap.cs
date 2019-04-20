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
            Map(x => x.Category);
            Map(x => x.Responsible);
            Map(x => x.Importance);
        }
    }
}
