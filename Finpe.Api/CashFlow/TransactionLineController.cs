using Finpe.Api.Utils;
using Finpe.CashFlow;
using Finpe.Parser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace Finpe.Api.CashFlow
{
    [Route("api/[controller]")]
    public class TransactionLineController : BaseController
    {
        private TransactionLineRepository transactionLineRepository;

        public TransactionLineController(UnitOfWork unitOfWork, TransactionLineRepository transactionLineRepository) : base(unitOfWork)
        {
            this.transactionLineRepository = transactionLineRepository;
        }

        [HttpPost("Upload")]
        public IActionResult Upload()
        {
            long size = 0;
            IFormFile file = Request.Form.Files[0];

            if (file.Length > 0)
            {
                StatementParser parser = new StatementParser();

                StreamReader reader = new StreamReader(file.OpenReadStream());
                string lines = reader.ReadToEnd();
                List<TransactionLine> transactinLines = parser.Parse(lines);
                transactinLines.ForEach(item => transactionLineRepository.Add(item));
                size += transactinLines.Count;
            }

            return Ok(new { transactionLines = size });
        }
    }
}
