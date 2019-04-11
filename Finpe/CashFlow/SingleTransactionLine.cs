using Finpe.Statement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finpe.CashFlow
{
    public class SingleTransactionLine : TransactionLine
    {
        public SingleTransactionLine(DateTime date, string description, decimal amount) : base(date, description, amount)
        {
        }
    }
}
