using System;

namespace Finpe.CashFlow
{
    public abstract class TransactionLine
    {
        public DateTime TransactionDate { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        
        public TransactionLine(TransactionLineInfo info)
        {
            TransactionDate = info.TransactionDate;
            Description = info.Description;
            Amount = info.Amount;
        }        
    }
}
