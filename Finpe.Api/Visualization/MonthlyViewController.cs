using Finpe.Api.Budget;
using Finpe.Api.CashFlow;
using Finpe.Api.Jwt;
using Finpe.Api.RecurringCashFlow;
using Finpe.Api.Utils;
using Finpe.Budget;
using Finpe.RecurringCashFlow;
using Finpe.Utils;
using Finpe.Visualization;
using Microsoft.AspNetCore.Authorization;
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
        private readonly MontlyBudgetRepository montlyBudgetRepository;

        public MonthlyViewController(UnitOfWork unitOfWork, 
            TransactionLineRepository transactionLineRepository, 
            RecurringTransactionRepository recurringTransactionRepository,
            MontlyBudgetRepository montlyBudgetRepository) : base(unitOfWork)
        {
            this.transactionLineRepository = transactionLineRepository;
            this.recurringTransactionRepository = recurringTransactionRepository;
            this.montlyBudgetRepository = montlyBudgetRepository;
        }

        [HttpGet]
        [Authorize(Permissions.ViewAll)]
        public IActionResult GetList()
        {
            List<MonthlyView> months = new MonthlyViewBuilder(
                    transactionLineRepository.GetList().ToList(),
                    new List<IViewerPipeline>()
                    {
                        new RecurringTransactionsPipeline(recurringTransactionRepository.GetList().ToList(), DateTime.Now.AddMonths(6).ToYearMonth()),
                        new MontlyBudgetPipeline(montlyBudgetRepository.GetList().ToList())
                    })
                .Build(-3_175.16m);

            return Ok(months);
        }

        [HttpPut]
        [Authorize(Permissions.ViewAll)]
        public IActionResult PutList(List<BudgetDto> budgets)
        {
            List<MonthlyView> months = new MonthlyViewBuilder(
                    transactionLineRepository.GetList().ToList(),
                    new List<IViewerPipeline>()
                    {
                        new RecurringTransactionsPipeline(recurringTransactionRepository.GetList().ToList(), DateTime.Now.AddMonths(6).ToYearMonth()),
                        new MontlyBudgetPipeline(ParseBudgets(budgets))
                    })
                .Build(-3_175.16m);

            return Ok(months);
        }

        private List<MontlyBudget> ParseBudgets(List<BudgetDto> budgets)
        {
            if (budgets == null)
            {
                return new List<MontlyBudget>();
            }

            return budgets
                .Select(x => new MontlyBudget(x.Category, x.Amount, x.Day))
                .ToList();
        }
    }
}
