using Finpe.CashFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finpe.RecurringCashFlow
{
    public class ExecutedRecurringTransactionLine : ExecutedTransactionLine
    {
        public ExecutedRecurringTransactionLine(DateTime date, string description, decimal amount) : base(date, description, amount)
        {
        }
    }
}
