using Finpe.Api.Jwt;
using Finpe.Api.Utils;
using Finpe.CashFlow;
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

        [HttpPost]
        [Authorize(Permissions.WriteAll)]
        public IActionResult AddStatement(TransactionDto dto)
        {
            var transactionLine = new SingleTransactionLine(
                new TransactionLineInfo(dto.Date, dto.Amount, dto.Description),
                new ClassificationInfo(dto.Category, dto.Responsible, dto.Importance));

            transactionLineRepository.Add(transactionLine);

            return Ok();
        }
    }
}
