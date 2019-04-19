using System.Collections.Generic;
using System.Linq;
using Finpe.CashFlow;
using Finpe.Utils;

namespace Finpe.RecurringCashFlow
{
    public class RecurringTransaction : Entity
    {
        public virtual string Description { get; private set; }
        public virtual decimal Amount { get; private set; }
        public virtual int Day { get; private set; }
        public virtual ClassificationInfo Classification { get; private set; }
        public virtual YearMonth StartYearMonth { get; set; }
        public virtual YearMonth EndYearMonth { get; set; }

        public RecurringTransaction(string description, decimal amount, int day, ClassificationInfo classification)
        {
            this.Description = description;
            this.Amount = amount;
            this.Day = day;
            this.Classification = classification;
        }        

        public void IncludeLines(List<TransactionLine> lines, YearMonth from, YearMonth to)
        {
            for (YearMonth i = ChooseInitialYearMonth(from); i <= ChooseFinalYearMonth(to); i = i.NextMonth())
            {
                if (!ExistsExecutedRecurringTransactionLine(lines, i))
                {
                    lines.Add(
                        new RecurringTransactionLine(new TransactionLineInfo(i.ToDate(Day), Amount, Description), Classification));
                }
            }
        }

        private bool ExistsExecutedRecurringTransactionLine(List<TransactionLine> lines, YearMonth yearMonth)
        {
            var test = lines.Where(x => x is ExecutedRecurringTransactionLine);

            return lines
                .Where(x => x is ExecutedRecurringTransactionLine)
                .Any(x => x.Description == Description && yearMonth.Equals(x.TransactionDate));
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
