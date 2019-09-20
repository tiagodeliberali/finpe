using Finpe.Api.Utils;
using Finpe.Budget;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finpe.Api.Budget
{
    [Route("api/[controller]")]
    [Authorize]
    public class BudgetController : BaseController
    {
        private MontlyBudgetRepository montlyBudgetRepository;

        public BudgetController(UnitOfWork unitOfWork, MontlyBudgetRepository montlyBudgetRepository) : base(unitOfWork)
        {
            this.montlyBudgetRepository = montlyBudgetRepository;
        }

        [HttpPost]
        public IActionResult AddBudget(BudgetDto budget)
        {
            var recurrence = new MontlyBudget(
                budget.Category,
                budget.Amount,
                budget.Day);

            montlyBudgetRepository.Add(recurrence);

            return Ok();
        }
    }
}
