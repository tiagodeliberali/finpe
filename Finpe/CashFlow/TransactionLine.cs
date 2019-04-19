using Finpe.Utils;
using System;

namespace Finpe.CashFlow
{
    public abstract class TransactionLine : Entity
    {
        protected virtual TransactionLineTypes TransactionLineType { get; set; }
        public virtual DateTime TransactionDate { get; private set; }
        public virtual decimal Amount { get; protected set; }
        public virtual string Description { get; private set; }
        
        public TransactionLine(TransactionLineInfo info)
        {
            TransactionDate = info.TransactionDate;
            Description = info.Description;
            Amount = info.Amount;
        }        
    }
}
