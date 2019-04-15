using System;

namespace Finpe.CashFlow
{
    public class TransactionLineInfo
    {
        public DateTime TransactionDate { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }

        public TransactionLineInfo(DateTime transactionDate, decimal amount, string description)
        {
            TransactionDate = transactionDate;
            Amount = amount;
            Description = description;
        }
    }
}
