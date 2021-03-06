﻿using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using FluentNHibernate.Mapping;

namespace Finpe.Api.MultilineCashflow
{
    public class ExecutedMultilineTransactionLineMap : SubclassMap<ExecutedMultilineTransactionLine>
    {
        public ExecutedMultilineTransactionLineMap()
        {
            DiscriminatorValue(TransactionLineTypes.MultilineExecuted);
            Map(x => x.Difference).Nullable();
        }
    }
}
