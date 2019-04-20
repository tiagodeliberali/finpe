using Finpe.Api.Utils;
using Finpe.CashFlow;

namespace Finpe.Api.CashFlow
{
    public class TransactionLineRepository : Repository<TransactionLine>
    {
        public TransactionLineRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
