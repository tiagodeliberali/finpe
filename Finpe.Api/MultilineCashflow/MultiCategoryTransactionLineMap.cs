using Finpe.MultilineCashflow;
using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace Finpe.Api.MultilineCashflow
{
    public class MultiCategoryTransactionLineMap : ClassMap<MultiCategoryTransactionLine>
    {
        private readonly string TransactionTypeName = "TransactionType";

        public MultiCategoryTransactionLineMap()
        {
            Id(x => x.Id);
            DiscriminateSubClassesOnColumn(TransactionTypeName);
            Map(Reveal.Member<MultiCategoryTransactionLine>(TransactionTypeName)).CustomType<int>();
            Map(x => x.TransactionDate);
            Map(x => x.Amount);
            Map(x => x.Description);
            HasMany(x => x.Lines);
        }
    }
}
