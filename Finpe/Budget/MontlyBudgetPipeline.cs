using Finpe.CashFlow;
using Finpe.Utils;
using Finpe.Visualization;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.Budget
{
    public class MontlyBudgetPipeline : IViewerPipeline
    {
        private List<MontlyBudget> budgets;
        private Dictionary<YearMonth, List<MontlyBudget>> budgetPerMonth = new Dictionary<YearMonth, List<MontlyBudget>>();

        public MontlyBudgetPipeline(List<MontlyBudget> budgets)
        {
            this.budgets = budgets;
        }

        public void ProcessLines(List<TransactionLine> statements)
        {
            YearMonth initialYearMonth = statements.Min(x => x.TransactionDate).ToYearMonth();
            YearMonth finalYearMonth = statements.Max(x => x.TransactionDate).ToYearMonth();

            for (YearMonth yearMonth = initialYearMonth; yearMonth <= finalYearMonth; yearMonth = yearMonth.NextMonth())
            {
                budgetPerMonth.Add(yearMonth, new List<MontlyBudget>());

                foreach (MontlyBudget budget in budgets)
                {
                    List<TransactionLine> monthStatements = statements.Where(x => yearMonth.Equals(x.TransactionDate)).ToList();

                    MontlyBudget currentBudget = budget.Process(monthStatements);
                    budgetPerMonth[yearMonth].Add(currentBudget);
                    currentBudget.IncludeLine(statements, yearMonth);                    
                }
            }            
        }

        public void ProcessViews(List<MonthlyView> monthViews)
        {
            foreach (MonthlyView view in monthViews)
            {
                view.Budgets = budgetPerMonth[view.YearMonth];
            }
        }
    }
}
