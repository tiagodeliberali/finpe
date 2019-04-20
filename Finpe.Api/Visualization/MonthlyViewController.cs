using Finpe.Api.CashFlow;
using Finpe.Api.Utils;
using Finpe.CashFlow;
using Finpe.Visualization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.Api.Visualization
{
    [Route("api/[controller]")]
    public class MonthlyViewController : BaseController
    {
        private readonly TransactionLineRepository transactionLineRepository;

        public MonthlyViewController(UnitOfWork unitOfWork, TransactionLineRepository transactionLineRepository) : base(unitOfWork)
        {
            this.transactionLineRepository = transactionLineRepository;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            List<MonthlyView> months = new MonthlyViewBuilder(
                    transactionLineRepository.GetList().ToList(), new List<IViewerPipeline>())
                .Build(-3_175.16m);

            return Ok(months);
        }
    }
}
