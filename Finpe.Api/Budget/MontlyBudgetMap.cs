using Finpe.Budget;
using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace Finpe.Api.Budget
{
    public class MontlyBudgetMap : ClassMap<MontlyBudget>
    {
        public MontlyBudgetMap()
        {
            Id(x => x.Id);
            Map(Reveal.Member<MontlyBudget>("ExecutionDay"));
            Map(Reveal.Member<MontlyBudget>("Category"));
            Map(x => x.Available);
        }
    }
}
