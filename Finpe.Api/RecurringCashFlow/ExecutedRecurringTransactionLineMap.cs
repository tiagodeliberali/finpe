using Finpe.CashFlow;
using Finpe.RecurringCashFlow;
using FluentNHibernate.Mapping;

namespace Finpe.Api.RecurringCashFlow
{
    public class ExecutedRecurringTransactionLineMap : SubclassMap<ExecutedRecurringTransactionLine>
    {
        public ExecutedRecurringTransactionLineMap()
        {
            DiscriminatorValue(TransactionLineTypes.ExecutedRecurring);
        }
    }
}
