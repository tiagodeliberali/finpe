using System.Collections.Generic;
using System.Linq;
using Finpe.CashFlow;
using Finpe.Utils;
using Finpe.Visualization;

namespace Finpe.RecurringCashFlow
{
    public class RecurringTransactionsPipeline : IViewerPipeline
    {
        private List<RecurringTransaction> recurringTransactions;
        private YearMonth finalYearMonth;

        public RecurringTransactionsPipeline(List<RecurringTransaction> recurringTransactions, YearMonth finalYearMonth)
        {
            this.recurringTransactions = recurringTransactions;
            this.finalYearMonth = finalYearMonth;
        }

        public void ProcessLines(List<TransactionLine> statements)
        {
            if (recurringTransactions == null) return;

            YearMonth initialYearMonth = statements.Min(x => x.TransactionDate).ToYearMonth();

            foreach (var recurringTransaction in recurringTransactions)
            {
                recurringTransaction.IncludeLines(statements, initialYearMonth, finalYearMonth);
            }
        }
    }
}
