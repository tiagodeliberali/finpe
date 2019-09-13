using Finpe.Budget;
using Finpe.CashFlow;
using Finpe.RecurringCashFlow;
using Finpe.Utils;
using Finpe.Visualization;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Finpe.Test
{
    public class MonthlyStatementTest
    {
        [Fact]
        public void CreateMonthlyStatement()
        {
            List<TransactionLine> statements = TransactionLineBuilder.BuildList()
                .Add(1_000m, "salary", day: 07, category: "Entrada")
                .Add(-800m, "aluguel", day: 10, category: "Educação")
                .Build();

            List<MonthlyView> months = new MonthlyViewBuilder(statements).Build(100m);

            Assert.Single(months);
            ValitadeMonth(months.First(), 4, 100m, 300m, 2);
        }

        [Fact]
        public void CreateMonthlyStatementWithNullTransactionLines()
        {
            List<MonthlyView> months = new MonthlyViewBuilder(null).Build(100m);

            Assert.Empty(months);
        }

        [Fact]
        public void CreateMonthlyStatementWithEmptyTransactionLines()
        {
            List<MonthlyView> months = new MonthlyViewBuilder(new List<TransactionLine>()).Build(100m);

            Assert.Empty(months);
        }

        [Fact]
        public void CreateMonthlyStatementShouldDiplayOrderedData()
        {
            List<TransactionLine> statements = TransactionLineBuilder.BuildList()
                .Add(-240m,  day: 22)
                .Add(1_000m, day: 07)
                .Add(-800m, day: 10)
                .Add(-90m, day: 09)
                .Build();

            List<MonthlyView> months = new MonthlyViewBuilder(statements).Build(100m);

            Assert.Equal(DateTime.Parse("2019-04-07"), months.First().Lines[0].TransactionDate);
            Assert.Equal(DateTime.Parse("2019-04-09"), months.First().Lines[1].TransactionDate);
            Assert.Equal(DateTime.Parse("2019-04-10"), months.First().Lines[2].TransactionDate);
            Assert.Equal(DateTime.Parse("2019-04-22"), months.First().Lines[3].TransactionDate);
        }

        [Fact]
        public void CreateMonthlyStatementForMoreMonths()
        {
            List<TransactionLine> statements = TransactionLineBuilder.BuildList()
                .Add(-800m, day: 22, month: 4)
                .Add(1_000m, day: 07, month: 4)
                .Add(-800m, day: 22, month: 5)
                .Add(1_000m, day: 07, month: 5)
                .Add(-800m, day: 22, month: 6)
                .Add(1_000m, day: 07, month: 6)
                .Build();

            List<MonthlyView> months = new MonthlyViewBuilder(statements).Build(100m);

            Assert.Equal(3, months.Count);
            ValitadeMonth(months[0], 4, 100m, 300m, 2);
            ValitadeMonth(months[1], 5, 300m, 500m, 2);
            ValitadeMonth(months[2], 6, 500m, 700m, 2);
        }

        [Fact]
        public void CreateMonthlyStatementForMoreMonthsFillingEmptyMonths()
        {
            List<TransactionLine> statements = TransactionLineBuilder.BuildList()
                .Add(-800m, day: 22, month: 4)
                .Add(1_000m, day: 07, month: 4)
                .Add(-800m, day: 22, month: 6)
                .Add(1_000m, day: 07, month: 6)
                .Build();

            List<MonthlyView> months = new MonthlyViewBuilder(statements).Build(100m);

            Assert.Equal(3, months.Count);
            ValitadeMonth(months[0], 4, 100m, 300m, 2);
            ValitadeMonth(months[1], 5, 300m, 300m, 0);
            ValitadeMonth(months[2], 6, 300m, 500m, 2);
        }

        [Fact]
        public void CreateMonthlyStatementWithRecurringInformation()
        {
            List<RecurringTransaction> recurringTransactions = new List<RecurringTransaction>()
            {
                new RecurringTransaction("Conta de Luz", -200m, 15, new ClassificationInfo("Moradia", ClassificationInfo.ResponsibleAll, Importance.Essential))
            };

            List<TransactionLine> statements = TransactionLineBuilder.BuildList()
                .Add(TransactionLineBuilder.BuildExecutedRecurringTransactionLine(-100m, description: "Conta de Luz", day: 15, month: 4))
                .Add(-800m, day: 22, month: 4)
                .Add(1_000m, day: 07, month: 4)
                .Add(-800m, day: 22, month: 6)
                .Add(1_000m, day: 07, month: 6)
                .Build();

            List<MonthlyView> months = new MonthlyViewBuilder(statements, 
                    new List<IViewerPipeline>() { new RecurringTransactionsPipeline(recurringTransactions, new YearMonth(2019, 7)) })
                .Build(100m);

            Assert.Equal(4, months.Count);
            ValitadeMonth(months[0], 4, 100m, 200m, 3);
            ValitadeMonth(months[1], 5, 200m, 0m, 1);
            ValitadeMonth(months[2], 6, 0m, 0m, 3);
            ValitadeMonth(months[3], 7, 0m, -200m, 1);
        }

        [Fact]
        public void CreateMonthlyWithOnlyRecurringInformation()
        {
            List<RecurringTransaction> recurringTransactions = new List<RecurringTransaction>()
            {
                new RecurringTransaction("Conta de Luz", -200m, 15, new ClassificationInfo("Moradia", ClassificationInfo.ResponsibleAll, Importance.Essential))
                {
                    StartYearMonth = new YearMonth(2019, 5)
                }
            };

            List<TransactionLine> statements = new List<TransactionLine>();

            List<MonthlyView> months = new MonthlyViewBuilder(statements,
                    new List<IViewerPipeline>() { new RecurringTransactionsPipeline(recurringTransactions, new YearMonth(2019, 7)) })
                .Build(100m);

            Assert.Equal(3, months.Count);
            ValitadeMonth(months[0], 5, 100m, -100m, 1);
            ValitadeMonth(months[1], 6, -100m, -300m, 1);
            ValitadeMonth(months[2], 7, -300m, -500m, 1);
        }

        [Fact]
        public void CreateMonthlyStatementWithEmptyInformation()
        {
            List<RecurringTransaction> recurringTransactions = new List<RecurringTransaction>();
            List<TransactionLine> statements = new List<TransactionLine>();

            List<MonthlyView> months = new MonthlyViewBuilder(statements,
                    new List<IViewerPipeline>() { new RecurringTransactionsPipeline(recurringTransactions, new YearMonth(2019, 7)) })
                .Build(100m);

            Assert.Empty(months);
        }

        [Fact]
        public void CreateMonthlyStatementWithMonthlyBudget()
        {
            List<MontlyBudget> budgets = new List<MontlyBudget>()
            {
                new MontlyBudget(TransactionLineBuilder.DefaultCategory, 1_000m, 15)
            };

            List<TransactionLine> statements = TransactionLineBuilder.BuildList()
                .Add(-200m, day: 29, month: 4)
                .Add(-800m, day: 22, month: 4)
                .Add(1_000m, day: 07, month: 4, category: "Entrada")
                .Add(-800m, day: 22, month: 6)
                .Add(1_000m, day: 07, month: 6, category: "Entrada")
                .Build();

            List<MonthlyView> months = new MonthlyViewBuilder(
                    statements, new List<IViewerPipeline>() { new MontlyBudgetPipeline(budgets) })
                .Build(100m);

            Assert.Equal(3, months.Count);
            ValitadeMonth(months[0], 4, 100m, 100m, 3);
            ValitadeMonth(months[1], 5, 100m, -900m, 1);
            ValitadeMonth(months[2], 6, -900m, -900m, 3);

            Assert.Single(months[0].Budgets);
            Assert.Equal(0m, months[0].Budgets.First().Available);
            Assert.Equal(1_000m, months[0].Budgets.First().Used);

            Assert.Single(months[1].Budgets);
            Assert.Equal(1_000m, months[1].Budgets.First().Available);
            Assert.Equal(0m, months[1].Budgets.First().Used);

            Assert.Single(months[2].Budgets);
            Assert.Equal(200m, months[2].Budgets.First().Available);
            Assert.Equal(800m, months[2].Budgets.First().Used);
        }

        private void ValitadeMonth(MonthlyView monthView, int month, decimal initialAmount, decimal finalAmount, int numberOfRows)
        {
            Assert.Equal(new YearMonth(2019, month), monthView.YearMonth);
            Assert.Equal(initialAmount, monthView.InitialAmount);
            Assert.Equal(finalAmount, monthView.FinalAmount);
            Assert.Equal(numberOfRows, monthView.Lines.Count);
        }
    }
}
