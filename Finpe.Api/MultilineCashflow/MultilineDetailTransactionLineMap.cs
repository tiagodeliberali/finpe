using Finpe.Api.Utils;
using Finpe.CashFlow;
using Finpe.MultilineCashflow;

namespace Finpe.Api.MultilineCashflow
{
    public class MultilineDetailTransactionLineMap : ClassifiedTransactionLineSubclassMap<MultilineDetailTransactionLine>
    {
        public MultilineDetailTransactionLineMap() : base()
        {
            DiscriminatorValue(TransactionLineTypes.MultilineDetail);
            References(x => x.Parent).Column("MultiCategoryTransactionLineID");
        }
    }
}
