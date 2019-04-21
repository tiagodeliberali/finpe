using Finpe.Api.Utils;
using Finpe.CashFlow;
using FluentNHibernate.Mapping;

namespace Finpe.Api.CashFlow
{
    public class SingleTransactionLineMap : ClassifiedTransactionLineSubclassMap<SingleTransactionLine>
    {
        public SingleTransactionLineMap() : base()
        {
            DiscriminatorValue(TransactionLineTypes.Single);
        }
    }
}
