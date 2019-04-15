using System;

namespace Finpe.CashFlow
{
    public abstract class ClassifiedTransactionLine : TransactionLine
    {
        public ClassifiedTransactionLine(DateTime date, string description, decimal amount) : base(date, description, amount)
        {
            Category = "";
            Responsible = "";
            Importance = Importance.NotDefined;
        }

        public string Category { get; private set; }
        public string Responsible { get; private set; }
        public Importance Importance { get; private set; }

        public void Classify(string category, string responsible, Importance importance)
        {
            Category = category;
            Responsible = responsible;
            Importance = importance;
        }
    }
}
