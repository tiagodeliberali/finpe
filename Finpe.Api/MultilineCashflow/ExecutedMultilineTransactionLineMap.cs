using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using FluentNHibernate.Mapping;

namespace Finpe.Api.MultilineCashflow
{
    public class ExecutedMultilineTransactionLineMap : SubclassMap<ExecutedMultilineTransactionLine>
    {
        public ExecutedMultilineTransactionLineMap()
        {
            DiscriminatorValue(TransactionLineTypes.ExecutedMultiline);
            Map(x => x.Difference);
            HasMany(x => x.Lines);
        }
    }
}
