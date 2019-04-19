using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using FluentNHibernate.Mapping;

namespace Finpe.Api.MultilineCashflow
{
    public class MultilineTransactionLineMap : SubclassMap<MultilineTransactionLine>
    {
        public MultilineTransactionLineMap()
        {
            DiscriminatorValue(TransactionLineTypes.Multiline);
        }
    }
}
