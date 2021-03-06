﻿using Finpe.Api.Utils;
using Finpe.CashFlow;
using Finpe.RecurringCashFlow;

namespace Finpe.Api.RecurringCashFlow
{
    public class RecurringTransactionLineMap : ClassifiedTransactionLineSubclassMap<RecurringTransactionLine>
    {
        public RecurringTransactionLineMap() : base()
        {
            DiscriminatorValue(TransactionLineTypes.Recurring);
        }
    }
}
