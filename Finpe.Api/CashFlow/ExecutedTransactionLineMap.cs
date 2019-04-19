using Finpe.CashFlow;
using FluentNHibernate.Mapping;

namespace Finpe.Api.CashFlow
{
    public class ExecutedTransactionLineMap : SubclassMap<ExecutedTransactionLine>
    {
        public ExecutedTransactionLineMap()
        {
            DiscriminatorValue(TransactionLineTypes.Executed);
            Map(x => x.Difference);
        }
    }
}
