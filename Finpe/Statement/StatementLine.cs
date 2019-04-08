using System;

namespace Finpe.Statement
{
    public abstract class StatementLine
    {
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public DateTime TransactionDate { get; private set; }

        public StatementLine(string description, decimal amount, DateTime transactionDate)
        {
            Description = description;
            Amount = amount;
            TransactionDate = transactionDate;
        }

        public abstract decimal CalculateNewAmount(decimal finalAmount);
    }
}
