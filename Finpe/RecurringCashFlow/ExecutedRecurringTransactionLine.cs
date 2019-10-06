using Finpe.CashFlow;
using System;

namespace Finpe.RecurringCashFlow
{
    public class ExecutedRecurringTransactionLine : ExecutedTransactionLine
    {
        public ExecutedRecurringTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        public ExecutedRecurringTransactionLine(TransactionLineInfo info, ClassificationInfo classification) : base(info, classification)
        {
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
