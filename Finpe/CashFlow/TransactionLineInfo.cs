using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace Finpe.CashFlow
{
    public class TransactionLineInfo : ValueObject
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

        protected TransactionLineInfo()
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TransactionDate;
            yield return Amount;
            yield return Description;
        }
    }
}
