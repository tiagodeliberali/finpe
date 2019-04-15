using System;

namespace Finpe.CashFlow
{
    public class ReccurentTransactionLine : ClassifiedTransactionLine
    {
        public ReccurentTransactionLine(DateTime date, string description, decimal amount) : base(date, description, amount)
        {
        }
    }
}
