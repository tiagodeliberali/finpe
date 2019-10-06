using Finpe.Api.Utils;
using Finpe.CashFlow;

namespace Finpe.Api.CashFlow
{
    public class ExecutedTransactionLineMap : ClassifiedTransactionLineSubclassMap<ExecutedTransactionLine>
    {
        public ExecutedTransactionLineMap()
        {
            DiscriminatorValue(TransactionLineTypes.Executed);
            Map(x => x.Difference).Nullable();
        }
    }
}
