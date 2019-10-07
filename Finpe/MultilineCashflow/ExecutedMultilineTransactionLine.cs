using Finpe.CashFlow;
using System.Linq;

namespace Finpe.MultilineCashflow
{
    public class ExecutedMultilineTransactionLine : MultiCategoryTransactionLine
    {
        public virtual decimal Difference { get; protected set; }

        public ExecutedMultilineTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        protected ExecutedMultilineTransactionLine() : base()
        {
        }

        public ExecutedMultilineTransactionLine(MultilineTransactionLine multilineTransaction, ExecutedTransactionLine statementLine)
            : base(new TransactionLineInfo(statementLine.TransactionDate, statementLine.Amount, multilineTransaction.Description))
        {
            Difference = multilineTransaction.Amount - statementLine.Amount;
            _lines = multilineTransaction.Lines.ToList();
        }

        public override void Remove(MultilineDetailTransactionLine transactionLine)
        {
        }
    }
}
