using Finpe.Api.Utils;
using Finpe.Budget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.Api.Budget
{
    public class MontlyBudgetRepository : Repository<MontlyBudget>
    {
        public MontlyBudgetRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IReadOnlyList<MontlyBudget> GetList()
        {
            return _unitOfWork
                .Query<MontlyBudget>()
                .ToList();
        }

        public MontlyBudget GetByCategory(string category)
        {
            return _unitOfWork
                .Query<MontlyBudget>()
                .Where(x => x.Category == category)
                .FirstOrDefault();
        }
    }
}
