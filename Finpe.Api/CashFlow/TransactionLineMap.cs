using Finpe.CashFlow;
using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace Finpe.Api.CashFlow
{
    public class TransactionLineMap : ClassMap<TransactionLine>
    {
        private readonly string TransactionTypeName = "TransactionLineType";
        public TransactionLineMap()
        {
            Id(x => x.Id);
            DiscriminateSubClassesOnColumn(TransactionTypeName);
            
            Map(x => x.TransactionDate);
            Map(x => x.Amount);
            Map(x => x.Description);
        }
    }
}
