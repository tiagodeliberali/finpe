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
        private TransactionLineRepository transactionLineRepository;

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
    }
}
