using System.Collections.Generic;
using System.Linq;
using Finpe.CashFlow;
using Finpe.Utils;

namespace Finpe.RecurringCashFlow
{
    public class RecurringTransaction
    {
        private string description;
        private decimal amount;
        private int day;
        ClassificationInfo classification;

        public RecurringTransaction(string description, decimal amount, int day, ClassificationInfo classification)
        {
            this.description = description;
            this.amount = amount;
            this.day = day;
            this.classification = classification;
        }

        public YearMonth StartYearMonth { get; set; }
        public YearMonth EndYearMonth { get; set; }

        public void IncludeLines(List<TransactionLine> lines, YearMonth from, YearMonth to)
        {
            for (YearMonth i = ChooseInitialYearMonth(from); i <= ChooseFinalYearMonth(to); i = i.NextMonth())
            {
                if (!ExistsExecutedRecurringTransactionLine(lines, i))
                {
                    lines.Add(
                        new RecurringTransactionLine(new TransactionLineInfo(i.ToDate(day), amount, description), classification));
                }
            }
        }

        private bool ExistsExecutedRecurringTransactionLine(List<TransactionLine> lines, YearMonth yearMonth)
        {
            var test = lines.Where(x => x is ExecutedRecurringTransactionLine);

            return lines
                .Where(x => x is ExecutedRecurringTransactionLine)
                .Any(x => x.Description == description && yearMonth.Equals(x.TransactionDate));
        }

        private YearMonth ChooseInitialYearMonth(YearMonth yearMonth)
        {
            if (StartYearMonth == null) return yearMonth;
            if (StartYearMonth > yearMonth) return StartYearMonth;
            return yearMonth;
        }

        private YearMonth ChooseFinalYearMonth(YearMonth yearMonth)
        {
            if (EndYearMonth == null) return yearMonth;
            if (EndYearMonth < yearMonth) return EndYearMonth;
            return yearMonth;
        }
    }
}
