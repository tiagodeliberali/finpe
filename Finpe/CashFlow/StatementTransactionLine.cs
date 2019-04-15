using System;

namespace Finpe.CashFlow
{
    public class StatementTransactionLine : TransactionLine
    {
        public StatementTransactionLine(DateTime date, string description, decimal amount) : base(date, description, amount)
        {
        }
    }
}
