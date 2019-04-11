using System;

namespace Finpe.CashFlow
{
    public abstract class TransactionLine
    {
        public DateTime TransactionDate { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public string Responsible { get; private set; }
        public Importance Importance { get; private set; }

        public TransactionLine(DateTime date, string description, decimal amount)
        {
            TransactionDate = date;
            Description = description;
            Amount = amount;
            Category = "";
            Responsible = "";
            Importance = Importance.NotDefined;
        }
    }
}
