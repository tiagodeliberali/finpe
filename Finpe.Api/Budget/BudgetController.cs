using Finpe.Api.Jwt;
using Finpe.Api.Utils;
using Finpe.Budget;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finpe.Api.Budget
{
    [Route("api/[controller]")]
    public class BudgetController : BaseController
    {
        private MontlyBudgetRepository montlyBudgetRepository;

        public BudgetController(UnitOfWork unitOfWork, MontlyBudgetRepository montlyBudgetRepository) : base(unitOfWork)
        {
            this.montlyBudgetRepository = montlyBudgetRepository;
        }

        [HttpPost]
        [Authorize(Permissions.WriteAll)]
        public IActionResult AddBudget(BudgetDto budget)
        {
            var recurrence = new MontlyBudget(
                budget.Category,
                budget.Amount,
                budget.Day);

            montlyBudgetRepository.Add(recurrence);

            return Ok();
        }

        [HttpPut]
        [Authorize(Permissions.WriteAll)]
        public IActionResult UpdateBudget(BudgetDto dto)
        {
            MontlyBudget budget = 
                dto.Id == 0
                ? montlyBudgetRepository.GetByCategory(dto.Category)
                : montlyBudgetRepository.GetById(dto.Id);

            budget.Category = dto.Category;
            budget.Available = dto.Amount;
            budget.ExecutionDay = dto.Day;

            return Ok();
        }

        [HttpGet]
        [Authorize(Permissions.ViewAll)]
        public IActionResult GetBudgets() => Ok(montlyBudgetRepository.GetList());
    }
}
