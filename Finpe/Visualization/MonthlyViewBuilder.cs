using Finpe.CashFlow;
using Finpe.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.Visualization
{
    public class MonthlyViewBuilder
    {
        private List<IViewerPipeline> pipelines;
        private List<TransactionLine> statements;

        public MonthlyViewBuilder(List<TransactionLine> statements) : this(statements, new List<IViewerPipeline>())
        {
        }

        public MonthlyViewBuilder(List<TransactionLine> statements, List<IViewerPipeline> pipelines) 
        {
            this.statements = statements;
            this.pipelines = pipelines;
        }    

        public List<MonthlyView> Build(decimal initialAmount)
        {
            foreach (IViewerPipeline pipe in pipelines)
            {
                pipe.ProcessLines(statements);
            }
            return BuildMonthlyView(initialAmount);
        }

        private List<MonthlyView> BuildMonthlyView(decimal initialAmount)
        {
            if (statements == null) return new List<MonthlyView>();

            List<MonthlyView> result = new List<MonthlyView>();

            decimal previousAmount = initialAmount;
            foreach (var yearMonth in GetMonthYearList())
            {
                List<TransactionLine> currentMonthLines = statements.Where(x => yearMonth.Equals(x.TransactionDate)).OrderBy(x => x.TransactionDate).ToList();
                MonthlyView month = BuildMonth(yearMonth, previousAmount, currentMonthLines);
                result.Add(month);
                previousAmount = month.FinalAmount;
            }

            return result;
        }

        private List<YearMonth> GetMonthYearList()
        {
            YearMonth min = statements.OrderBy(x => x.TransactionDate).First().TransactionDate.ToYearMonth();
            YearMonth max = statements.OrderBy(x => x.TransactionDate).Last().TransactionDate.ToYearMonth();

            List<YearMonth> results = new List<YearMonth>();

            for (YearMonth i = min; i <= max; i = i.NextMonth())
            {
                results.Add(i);
            }

            return results;
        }

        private MonthlyView BuildMonth(YearMonth yearMonth, decimal initialAmount, List<TransactionLine> monthlyStatements)
        {
            MonthlyView result = new MonthlyView(yearMonth, initialAmount);

            foreach (var item in monthlyStatements)
            {
                result.Add(item);
            }

            return result;
        }
    }
}
