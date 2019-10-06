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
        public IActionResult Consolidate(EditRecurrencyDto dto)
        {
            var transaction = recurringTransactionRepository.GetById(dto.Id);

            if (transaction == null)
            {
                return this.Error("Recurring transaction not found");
            }

            var consolidatedLine = new ExecutedRecurringTransactionLine(
                new TransactionLineInfo(new DateTime(dto.Year, dto.Month, transaction.Day), dto.Amount, transaction.Description),
                transaction.Classification);

            transactionLineRepository.Add(consolidatedLine);

            return Ok();
        }

        [HttpDelete("transactionLine")]
        [Authorize(Permissions.WriteAll)]
        public IActionResult DeleteTransactionLine(EditRecurrencyDto dto)
        {
            var transaction = recurringTransactionRepository.GetById(dto.Id);

            if (transaction == null)
            {
                return this.Error("Recurring transaction not found");
            }

            var consolidatedLine = new ExecutedRecurringTransactionLine(
                new TransactionLineInfo(new DateTime(dto.Year, dto.Month, transaction.Day), 0, transaction.Description),
                transaction.Classification);

            transactionLineRepository.Add(consolidatedLine);

            return Ok();
        }

        [HttpDelete()]
        [Authorize(Permissions.WriteAll)]
        public IActionResult Delete(EditRecurrencyDto dto)
        {
            var transaction = recurringTransactionRepository.GetById(dto.Id);

            if (transaction == null)
            {
                return this.Error("Recurring transaction not found");
            }

            recurringTransactionRepository.Delete(transaction);

            return Ok();
        }
    }
}
