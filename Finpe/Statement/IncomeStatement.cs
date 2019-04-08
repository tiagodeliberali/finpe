using System;

namespace Finpe.Statement
{
    public class IncomeStatement : BaseStatement
    {
        public IncomeStatement(string description, decimal amount, DateTime transactionDate) 
            : base(description, amount, transactionDate)
        {

        }
    }
}
