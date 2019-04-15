using System;

namespace Finpe.CashFlow
{
    public class SingleTransactionLine : ClassifiedTransactionLine
    {
        public SingleTransactionLine(string description, decimal amount, DateTime date) : base(date, description, amount)
        {
        }

        public RealizedTransactionLine Consolidate(StatementTransactionLine statementLine)
        {
            return new RealizedTransactionLine(this, statementLine);
        }
    }
}
