using System;

namespace Finpe.Statement
{
    public class OutcomeStatementLine : StatementLine
    {
        public OutcomeStatementLine(string description, decimal amount, DateTime transactionDate)
            : base(description, amount, transactionDate)
        {

        }
    }
}
