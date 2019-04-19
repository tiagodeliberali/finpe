using Finpe.CashFlow;
using Finpe.RecurringCashFlow;
using FluentNHibernate.Mapping;

namespace Finpe.Api.RecurringCashFlow
{
    public class RecurringTransactionLineMap : SubclassMap<RecurringTransactionLine>
    {
        public RecurringTransactionLineMap()
        {
            DiscriminatorValue(TransactionLineTypes.Recurring);
        }
    }
}
