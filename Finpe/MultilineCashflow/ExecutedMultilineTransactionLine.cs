using Finpe.CashFlow;
using System.Linq;

namespace Finpe.MultilineCashflow
{
    public class ExecutedMultilineTransactionLine : MultiCategoryTransactionLine
    {
        public decimal Difference { get; private set; }

        public ExecutedMultilineTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        public ExecutedMultilineTransactionLine(MultilineTransactionLine multilineTransaction, ExecutedTransactionLine statementLine)
            : base(new TransactionLineInfo(statementLine.TransactionDate, statementLine.Amount, multilineTransaction.Description))
        {
            Difference = multilineTransaction.Amount - statementLine.Amount;
            lines = multilineTransaction.Lines.ToList();
        }
    }
}
