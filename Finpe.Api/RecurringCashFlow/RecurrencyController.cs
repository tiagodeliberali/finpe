using Finpe.Api.Utils;
using Finpe.CashFlow;
using Finpe.RecurringCashFlow;
using Finpe.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finpe.Api.RecurringCashFlow
{
    [Route("api/[controller]")]
    public class RecurrencyController : BaseController
    {
        private RecurringTransactionRepository recurringTransactionRepository;

        public RecurrencyController(UnitOfWork unitOfWork, RecurringTransactionRepository recurringTransactionRepository) : base(unitOfWork)
        {
            this.recurringTransactionRepository = recurringTransactionRepository;
        }

        [HttpPost]
        [Authorize("write:all")]
        public IActionResult AddStatement(RecurrencyDto statement)
        {
            var recurrence = new RecurringTransaction(
                statement.Description,
                statement.Amount,
                statement.Date.Day,
                new ClassificationInfo(statement.Category, statement.Responsible, statement.Importance))
            {
                StartYearMonth = statement.Date.ToYearMonth()
            };

            if (statement.EndDate.HasValue)
            {
                recurrence.EndYearMonth = statement.EndDate.Value.ToYearMonth();
            }

            recurringTransactionRepository.Add(recurrence);

            return Ok();
        }
    }
}
