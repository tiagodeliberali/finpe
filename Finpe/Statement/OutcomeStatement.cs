using System;

namespace Finpe.Statement
{
    public class OutcomeStatement : BaseStatement
    {
        public OutcomeStatement(string description, decimal amount, DateTime transactionDate)
            : base(description, amount, transactionDate)
        {

        }
    }
}
