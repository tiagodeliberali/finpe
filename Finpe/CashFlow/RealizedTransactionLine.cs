﻿using Finpe.Statement;

namespace Finpe.CashFlow
{
    public class RealizedTransactionLine : TransactionLine
    {
        public RealizedTransactionLine(StatementLine statementLine)
            : base(statementLine.TransactionDate, statementLine.Description, statementLine.Amount)
        {
            Difference = 0m;
            StatementLine = statementLine;
        }

        public decimal Difference { get; private set; }
        public StatementLine StatementLine { get; set; }
    }
}
