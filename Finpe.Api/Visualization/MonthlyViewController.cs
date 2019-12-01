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
            List<MonthlyView> months = BuildStatements(montlyBudgetRepository.GetList().ToList());
            return Ok(months);
        }

        [HttpPut]
        [Authorize(Permissions.ViewAll)]
        public IActionResult PutList(List<BudgetDto> budgets)
        {
            List<MonthlyView> months = BuildStatements(ParseBudgets(budgets));
            return Ok(months);
        }

        private List<MonthlyView> BuildStatements(List<MontlyBudget> budgets)
        {
            DateTime currentDate = DateTime.Now;
            DateTime endDate = currentDate.AddMonths(6);

            return new MonthlyViewBuilder(
                    transactionLineRepository.GetList(currentDate, endDate).ToList(),
                    new List<IViewerPipeline>()
                    {
                        new RecurringTransactionsPipeline(recurringTransactionRepository.GetList().ToList(), endDate.ToYearMonth()),
                        new MontlyBudgetPipeline(budgets)
                    })
                .Build(0);
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
