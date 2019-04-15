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
        private string category;
        private string responsible;
        private Importance importance;

        public RecurringTransaction(string description, decimal amount, int day, string category, string responsible, Importance importance)
        {
            this.description = description;
            this.amount = amount;
            this.day = day;
            this.category = category;
            this.responsible = responsible;
            this.importance = importance;
        }

        public void IncludeLines(List<TransactionLine> lines, YearMonth from, YearMonth to)
        {
            for (YearMonth i = from; i <= to; i = i.NextMonth())
            {
                if (!ExistsExecutedRecurringTransactionLine(lines, i))
                {
                    lines.Add(
                        new RecurringTransactionLine(
                            new TransactionLineInfo(i.ToDate(day), amount, description), 
                            new ClassificationInfo(category, responsible, importance)));
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
    }
}
