using Finpe.CashFlow;
using FluentNHibernate.Mapping;

namespace Finpe.Api.Utils
{
    public class ClassifiedTransactionLineSubclassMap<T> : SubclassMap<T> where T : ClassifiedTransactionLine
    {
        public ClassifiedTransactionLineSubclassMap()
        {
            Map(x => x.Category).Nullable();
            Map(x => x.Responsible).Nullable();
            Map(x => x.Importance).Nullable();
        }
    }
}
