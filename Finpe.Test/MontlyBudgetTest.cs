using Finpe.Budget;
using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using System.Collections.Generic;
using Xunit;

namespace Finpe.Test
{
    public class MontlyBudgetTest
    {
        [Fact]
        public void GetBudgetInfo()
        {
            List<TransactionLine> lines = TransactionLineBuilder.BuildList()
                .Add(-300m, "Eletropaulo", day: 14)
                .Add(-500m, "Faculdade", day: 18, category: "Educação")
                .Add(-800m, "Aluguel", day: 22)
                .Build();

            MontlyBudget budget = new MontlyBudget(TransactionLineBuilder.DefaultCategory, 2_000m, 15);

            MontlyBudget processedBudget = budget.Process(lines);

            Assert.Equal(0m, budget.Used);
            Assert.Equal(2_000m, budget.Available);

            Assert.Equal(1_100m, processedBudget.Used);
            Assert.Equal(900m, processedBudget.Available);
        }

        [Fact]
        public void GetBudgetInfoWithMultilineTransaction()
        {
            MultilineTransactionLine line = TransactionLineBuilder.BuildMultilineTransactionLine();
            line.Add(TransactionLineBuilder.BuildSingleTransactionLine(-100m, "farmácia", category: "saude"));
            line.Add(TransactionLineBuilder.BuildSingleTransactionLine(-300m, "supermercado"));

            MultilineTransactionLine line2 = TransactionLineBuilder.BuildMultilineTransactionLine();
            line.Add(TransactionLineBuilder.BuildSingleTransactionLine(-300m, "farmácia", category: "saude"));
            line.Add(TransactionLineBuilder.BuildSingleTransactionLine(-500m, "supermercado"));

            ExecutedMultilineTransactionLine executedLine = line2.Consolidate(
                TransactionLineBuilder.BuildExecutedCreditcardTransactionLine(-1_000m));

            List<TransactionLine> lines = TransactionLineBuilder.BuildList()
                .Add(-300m, "Eletropaulo", day: 14)
                .Add(-500m, "Faculdade", day: 18, category: "Educação")
                .Add(line)
                .Add(executedLine)
                .Add(-800m, "Aluguel", day: 22)
                .Build();

            MontlyBudget budget = new MontlyBudget(TransactionLineBuilder.DefaultCategory, 2_000m, 15);

            MontlyBudget processedBudget = budget.Process(lines);

            Assert.Equal(1_900m, processedBudget.Used);
            Assert.Equal(100m, processedBudget.Available);
        }
    }
}
