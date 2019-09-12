using Finpe.Api.CashFlow;
using Finpe.Api.RecurringCashFlow;
using Finpe.Api.Utils;
using Finpe.RecurringCashFlow;
using Finpe.Utils;
using Finpe.Visualization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.Api.Visualization
{
    [Route("api/[controller]")]
    public class MonthlyViewController : BaseController
    {
        private readonly TransactionLineRepository transactionLineRepository;
        private readonly RecurringTransactionRepository recurringTransactionRepository;

        public MonthlyViewController(UnitOfWork unitOfWork, TransactionLineRepository transactionLineRepository, RecurringTransactionRepository recurringTransactionRepository) : base(unitOfWork)
        {
            this.transactionLineRepository = transactionLineRepository;
            this.recurringTransactionRepository = recurringTransactionRepository;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            List<MonthlyView> months = new MonthlyViewBuilder(
                    transactionLineRepository.GetList().ToList(),
                    new List<IViewerPipeline>()
                    {
                        new RecurringTransactionsPipeline(recurringTransactionRepository.GetList().ToList(), new YearMonth(DateTime.Now.Month >= 9 ? DateTime.Now.Year + 1 : DateTime.Now.Year, 12))
                    })
                .Build(-3_175.16m);

            return Ok(months);
        }
    }
}
