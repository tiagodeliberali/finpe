using Finpe.CashFlow;
using System;

namespace Finpe.RecurringCashFlow
{
    public class RecurringTransactionLine : ClassifiedTransactionLine
    {
        public RecurringTransactionLine(DateTime date, string description, decimal amount, string category, string responsible, Importance importance) 
            : base(date, description, amount)
        {
            Classify(category, responsible, importance);
        }
    }
}
