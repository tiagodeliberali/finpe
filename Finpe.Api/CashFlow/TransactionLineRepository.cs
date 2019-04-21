using Finpe.Api.Utils;
using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.Api.CashFlow
{
    public class TransactionLineRepository : Repository<TransactionLine>
    {
        public TransactionLineRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IReadOnlyList<TransactionLine> GetList()
        {
            return _unitOfWork
                .Query<TransactionLine>()
                .Where(x => !(x is MultilineDetailTransactionLine))
                .ToList();
        }
    }
}
