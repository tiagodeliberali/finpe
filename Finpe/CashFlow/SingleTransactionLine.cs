using Finpe.Statement;
using System;

namespace Finpe.CashFlow
{
    public class SingleTransactionLine : TransactionLine
    {
        public SingleTransactionLine(string description, decimal amount, DateTime date) : base(date, description, amount)
        {
        }

        public RealizedTransactionLine Consolidate(StatementLine statementLine)
        {
            return new RealizedTransactionLine(this, statementLine);
        }
    }
}
