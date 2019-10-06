using Finpe.CashFlow;
using System;

namespace Finpe.RecurringCashFlow
{
    public class ExecutedRecurringTransactionLine : ExecutedTransactionLine
    {
        public ExecutedRecurringTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        public ExecutedRecurringTransactionLine(decimal originalAmount, TransactionLineInfo info, ClassificationInfo classification) : base(info, classification)
        {
            Difference = originalAmount - info.Amount;
        }

        protected ExecutedRecurringTransactionLine() : base()
        {
        }

        public ExecutedRecurringTransactionLine(SingleTransactionLine singleTransactionLine, ExecutedTransactionLine statementLine)
            : base(new TransactionLineInfo(statementLine.TransactionDate, statementLine.Amount, singleTransactionLine.Description))
        {
        }
    }
}
