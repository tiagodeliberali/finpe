using Finpe.Api.CashFlow;
using Finpe.Api.Jwt;
using Finpe.Api.Utils;
using Finpe.CashFlow;
using Finpe.RecurringCashFlow;
using Finpe.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Finpe.Api.RecurringCashFlow
{
    [Route("api/[controller]")]
    public class RecurrencyController : BaseController
    {
        private readonly RecurringTransactionRepository recurringTransactionRepository;
        private readonly TransactionLineRepository transactionLineRepository;

        public RecurrencyController(UnitOfWork unitOfWork, 
            RecurringTransactionRepository recurringTransactionRepository,
            TransactionLineRepository transactionLineRepository) : base(unitOfWork)
        {
            this.recurringTransactionRepository = recurringTransactionRepository;
            this.transactionLineRepository = transactionLineRepository;
        }

        [HttpPost]
        [Authorize(Permissions.WriteAll)]
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

        [HttpPost("consolidate")]
        [Authorize(Permissions.WriteAll)]
        public IActionResult Consolidate(long id, decimal amount, int year, int month)
        {
            var transaction = recurringTransactionRepository.GetById(id);

            if (transaction == null)
            {
                return this.Error("Recurring transaction not found");
            }

            var consolidatedLine = new ExecutedRecurringTransactionLine(
                new TransactionLineInfo(new DateTime(year, month, transaction.Day), amount, transaction.Description));

            transactionLineRepository.Add(consolidatedLine);

            return Ok();
        }

        [HttpDelete("transactionLine")]
        [Authorize(Permissions.WriteAll)]
        public IActionResult DeleteTransactionLine(long id, int year, int month)
        {
            var transaction = recurringTransactionRepository.GetById(id);

            if (transaction == null)
            {
                return this.Error("Recurring transaction not found");
            }

            var consolidatedLine = new ExecutedRecurringTransactionLine(
                new TransactionLineInfo(new DateTime(year, month, transaction.Day), 0, transaction.Description));

            transactionLineRepository.Add(consolidatedLine);

            return Ok();
        }

        [HttpDelete()]
        [Authorize(Permissions.WriteAll)]
        public IActionResult Delete(long id)
        {
            var transaction = recurringTransactionRepository.GetById(id);

            if (transaction == null)
            {
                return this.Error("Recurring transaction not found");
            }

            recurringTransactionRepository.Delete(transaction);

            return Ok();
        }
    }
}
