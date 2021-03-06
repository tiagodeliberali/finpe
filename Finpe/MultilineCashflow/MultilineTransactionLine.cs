﻿using Finpe.CashFlow;

namespace Finpe.MultilineCashflow
{
    public class MultilineTransactionLine : MultiCategoryTransactionLine
    {
        public MultilineTransactionLine(TransactionLineInfo info) : base(info)
        {
        }

        protected MultilineTransactionLine() : base()
        {
        }

        public virtual void Add(MultilineDetailTransactionLine transactionLine)
        {
            transactionLine.Parent = this;
            _lines.Add(transactionLine);
            Amount += transactionLine.Amount;
        }

        public override void Remove(MultilineDetailTransactionLine transactionLine)
        {
            transactionLine.Parent = null;
            _lines.Remove(transactionLine);
            Amount -= transactionLine.Amount;
        }

        public virtual ExecutedMultilineTransactionLine Consolidate(ExecutedTransactionLine statementLine)
        {
            return new ExecutedMultilineTransactionLine(this, statementLine);
        }
    }
}
