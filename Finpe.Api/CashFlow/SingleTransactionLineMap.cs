using Finpe.CashFlow;
using FluentNHibernate.Mapping;

namespace Finpe.Api.CashFlow
{
    public class SingleTransactionLineMap : SubclassMap<SingleTransactionLine>
    {
        public SingleTransactionLineMap()
        {
            DiscriminatorValue(TransactionLineTypes.Single);
        }
    }
}
