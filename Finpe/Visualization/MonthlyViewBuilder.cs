using Finpe.CashFlow;
using Finpe.RecurringCashFlow;
using Finpe.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.Visualization
{
    public class MonthlyViewBuilder
    {
        private List<TransactionLine> statements;
        private List<RecurringTransaction> recurringTransactions;
        private YearMonth finalYearMonth;
        
        public MonthlyViewBuilder WithTransactionLines(List<TransactionLine> statements)
        {
            this.statements = statements;
            return this;
        }

        public MonthlyViewBuilder WithRecurringTransaction(List<RecurringTransaction> recurringTransactions, YearMonth finalYearMonth)
        {
            this.recurringTransactions = recurringTransactions;
            this.finalYearMonth = finalYearMonth;
            return this;
        }

        public List<MonthlyView> Build(decimal initialAmount)
        {
            BuildRecurringTransactions();
            return BuildMonthlyView(initialAmount);
        }

        private void BuildRecurringTransactions()
        {
            if (recurringTransactions == null) return;

            YearMonth initialYearMonth = statements.Select(x => x.TransactionDate).Min().ToYearMonth();

            foreach (var recurringTransaction in recurringTransactions)
            {
                recurringTransaction.IncludeLines(statements, initialYearMonth, finalYearMonth);
            }
        }

        private List<MonthlyView> BuildMonthlyView(decimal initialAmount)
        {
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
