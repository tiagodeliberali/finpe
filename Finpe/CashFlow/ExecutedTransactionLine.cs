using System;

namespace Finpe.CashFlow
{
    public class ExecutedTransactionLine : ClassifiedTransactionLine
    {
        public ExecutedTransactionLine(DateTime date, string description, decimal amount) : base(date, description, amount)
        {
            Difference = 0m;
        }

        public ExecutedTransactionLine(SingleTransactionLine singleTransactionLine, ExecutedTransactionLine statementLine)
            : base(statementLine.TransactionDate, singleTransactionLine.Description, statementLine.Amount)
        {
            Difference = singleTransactionLine.Amount - statementLine.Amount;
            Classify(singleTransactionLine.Category, singleTransactionLine.Responsible, singleTransactionLine.Importance);
        }

        public decimal Difference { get; private set; }
    }
}
