using Finpe.RecurringCashFlow;
using FluentNHibernate.Mapping;

namespace Finpe.Api.RecurringCashFlow
{
    public class RecurringTransactionMap : ClassMap<RecurringTransaction>
    {
        public RecurringTransactionMap()
        {
            Id(x => x.Id);
            Map(x => x.Description);
            Map(x => x.Amount);
            Map(x => x.Day);
            Component(x => x.StartYearMonth, m =>
            {
                m.Map(x => x.Month, "StartYearMonth_Month");
                m.Map(x => x.Year, "StartYearMonth_Year");
            });
            Component(x => x.EndYearMonth, m =>
            {
                m.Map(x => x.Month, "EndYearMonth_Month").Nullable();
                m.Map(x => x.Year, "EndYearMonth_Year").Nullable();
            });
            Component(x => x.Classification, m =>
            {
                m.Map(x => x.Category);
                m.Map(x => x.Responsible);
                m.Map(x => x.Importance);
            });
        }
    }
}
