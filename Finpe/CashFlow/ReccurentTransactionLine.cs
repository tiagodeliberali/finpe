using System;
using System.Collections.Generic;
using System.Text;

namespace Finpe.CashFlow
{
    public class ReccurentTransactionLine : TransactionLine
    {
        public ReccurentTransactionLine(DateTime date, string description, decimal amount) : base(date, description, amount)
        {
        }
    }
}
