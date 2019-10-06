using Finpe.Api.Jwt;
using Finpe.Api.Utils;
using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finpe.Api.CashFlow
{
    [Route("api/[controller]")]
    public class TransactionLineController : BaseController
    {
        private readonly TransactionLineRepository transactionLineRepository;

        public TransactionLineController(UnitOfWork unitOfWork,
            TransactionLineRepository transactionLineRepository) : base(unitOfWork)
        {
            this.transactionLineRepository = transactionLineRepository;
        }

        [HttpGet("multiline")]
        [Authorize(Permissions.ViewAll)]
        public IActionResult GetList()
        {
            return Ok(transactionLineRepository.GetMultilineList());
        }

        [HttpPost]
        [Authorize(Permissions.WriteAll)]
        public IActionResult AddStatement(TransactionDto dto)
        {
            if (!dto.IsMultiline)
            {
                transactionLineRepository.Add(new SingleTransactionLine(
                    new TransactionLineInfo(dto.Date, dto.Amount, dto.Description),
                    new ClassificationInfo(dto.Category, dto.Responsible, dto.Importance)));
                
            } 
            else if (dto.MultilineParentId.HasValue && dto.MultilineParentId.Value > 0)
            {
                MultilineTransactionLine line = (MultilineTransactionLine)transactionLineRepository.GetById(dto.MultilineParentId.Value);
                line.Add(new MultilineDetailTransactionLine(
                    new TransactionLineInfo(dto.Date, dto.Amount, dto.Description),
                    new ClassificationInfo(dto.Category, dto.Responsible, dto.Importance)));
                transactionLineRepository.Add(line);
            }
            else
            {
                transactionLineRepository.Add(new MultilineTransactionLine(
                    new TransactionLineInfo(dto.Date, dto.Amount, dto.Description)));
            }

            return Ok();
        }

        [HttpPost("consolidate")]
        [Authorize(Permissions.WriteAll)]
        public IActionResult Consolidate(EditTransactionDto dto)
        {
            var transaction = transactionLineRepository.GetById(dto.Id);
            
            if (transaction is SingleTransactionLine)
            {
                var consolidatedLine = ((SingleTransactionLine)transaction)
                    .Consolidate(new ExecutedTransactionLine(new TransactionLineInfo(transaction.TransactionDate, dto.Amount, transaction.Description)));

                transactionLineRepository.Add(consolidatedLine);
                transactionLineRepository.Delete(transaction);
            }
            else if (transaction is MultilineTransactionLine)
            {
                var consolidatedLine = ((MultilineTransactionLine)transaction)
                                    .Consolidate(new ExecutedTransactionLine(new TransactionLineInfo(transaction.TransactionDate, dto.Amount, transaction.Description)));

                transactionLineRepository.Add(consolidatedLine);
                transactionLineRepository.Delete(transaction);
            }
            else
            {
                return Error("Invalid type of transaction. Expected 'SingleTransactionLine' or 'MultilineTransactionLine' but found '" 
                    + transaction.GetType().Name + "'");
            }

            return Ok();
        }

        [HttpDelete()]
        [Authorize(Permissions.WriteAll)]
        public IActionResult Delete(EditTransactionDto dto)
        {
            var transaction = transactionLineRepository.GetById(dto.Id);

            if (transaction == null)
            {
                return this.Error("Transaction not found");
            }

            var transactionLines = transactionLineRepository.GetMultilineDetailTransactionLine(dto.Id);

            transactionLineRepository.Delete(transaction);

            foreach (var item in transactionLines)
            {
                transactionLineRepository.Delete(item);
            }

            return Ok();
        }
    }
}
