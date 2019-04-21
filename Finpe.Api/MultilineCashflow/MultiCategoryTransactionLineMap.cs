using Finpe.MultilineCashflow;
using FluentNHibernate.Mapping;

namespace Finpe.Api.MultilineCashflow
{
    public class MultiCategoryTransactionLineMap : SubclassMap<MultiCategoryTransactionLine>
    {
        public MultiCategoryTransactionLineMap()
        {
            HasMany(x => x.Lines).Access.CamelCaseField(Prefix.Underscore).Not.LazyLoad();
            Abstract();
        }
    }
}
