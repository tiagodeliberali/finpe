using Finpe.Api.Utils;
using Finpe.RecurringCashFlow;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.Api.RecurringCashFlow
{
    public class RecurringTransactionRepository : Repository<RecurringTransaction>
    {
        public RecurringTransactionRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IReadOnlyList<RecurringTransaction> GetList()
        {
            return _unitOfWork
                .Query<RecurringTransaction>()
                .ToList();
        }
    }
}
