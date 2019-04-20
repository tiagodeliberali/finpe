using Finpe.CashFlow;
using FluentNHibernate.Mapping;

namespace Finpe.Api.CashFlow
{
    public class SingleTransactionLineMap : SubclassMap<SingleTransactionLine>
    {
        public SingleTransactionLineMap()
        {
            DiscriminatorValue(TransactionLineTypes.Single);
            Map(x => x.Category);
            Map(x => x.Responsible);
            Map(x => x.Importance);
        }
    }
}
