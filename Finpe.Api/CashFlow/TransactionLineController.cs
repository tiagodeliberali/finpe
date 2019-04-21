using Finpe.Api.Utils;
using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using Finpe.Parser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace Finpe.Api.CashFlow
{
    [Route("api/[controller]")]
    public class TransactionLineController : BaseController
    {
        private TransactionLineRepository transactionLineRepository;
        private StatementParser statementParser;
        private CreditCardParser creditCardParser;

        public TransactionLineController(UnitOfWork unitOfWork, 
            TransactionLineRepository transactionLineRepository,
            StatementParser statementParser,
            CreditCardParser creditCardParser) : base(unitOfWork)
        {
            this.transactionLineRepository = transactionLineRepository;
            this.statementParser = statementParser;
            this.creditCardParser = creditCardParser;
        }

        [HttpPost("UploadStatement")]
        public IActionResult UploadStatement()
        {
            return processUploadedFiles((name, content) => statementParser.Parse(content));
        }

        [HttpPost("UploadCreditCard")]
        public IActionResult UploadCreditCard()
        {
            return processUploadedFiles((name, content) => {
                MultilineTransactionLine transactionLine = creditCardParser.ParseName(name);
                creditCardParser.Parse(content).ForEach(item => transactionLine.Add(item as MultilineDetailTransactionLine));

                return new List<TransactionLine>()
                {
                    transactionLine
                };
            });
        }

        private IActionResult processUploadedFiles(Func<string, string, List<TransactionLine>> processContentString)
        {
            long numberOfTransactinLines = 0;
            long numberOfFiles = 0;

            foreach (IFormFile file in Request.Form.Files)
            {
                numberOfFiles++;
                if (file.Length > 0)
                {
                    StreamReader reader = new StreamReader(file.OpenReadStream());
                    string lines = reader.ReadToEnd();
                    List<TransactionLine> transactinLines = processContentString(file.FileName, lines);
                    transactinLines.ForEach(item => transactionLineRepository.Add(item));
                    numberOfTransactinLines += transactinLines.Count;
                }
            }

            return Ok(new { files = numberOfFiles, transactionLines = numberOfTransactinLines });
        }
    }
}
