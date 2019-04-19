using Finpe.CashFlow;
using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace Finpe.Api.CashFlow
{
    public class ClassifiedTransactionLineMap : ClassMap<ClassifiedTransactionLine>
    {
        private readonly string TransactionTypeName = "TransactionType";
        public ClassifiedTransactionLineMap()
        {
            Id(x => x.Id);
            DiscriminateSubClassesOnColumn(TransactionTypeName);
            Map(Reveal.Member<ClassifiedTransactionLine>(TransactionTypeName)).CustomType<int>();

            Map(x => x.TransactionDate);
            Map(x => x.Amount);
            Map(x => x.Description);
            Map(x => x.Category);
            Map(x => x.Responsible);
            Map(x => x.Importance);
        }
    }
}
