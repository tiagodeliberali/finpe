using System;

namespace Finpe.CashFlow
{
    public class SingleTransactionLine : ClassifiedTransactionLine
    {
        public SingleTransactionLine(string description, decimal amount, DateTime date) : base(date, description, amount)
        {
        }

        public ExecutedTransactionLine Consolidate(ExecutedTransactionLine statementLine)
        {
            return new ExecutedTransactionLine(this, statementLine);
        }
    }
}
