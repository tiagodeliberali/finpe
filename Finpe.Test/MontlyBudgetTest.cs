using Finpe.Budget;
using Finpe.CashFlow;
using System;
using System.Collections.Generic;
using Xunit;

namespace Finpe.Test
{
    public class MontlyBudgetTest
    {
        private const string category = "Moradia";

        [Fact]
        public void GetBudgetInfo()
        {
            List<TransactionLine> lines = new List<TransactionLine>()
            {
                new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 14), -300m, "Eletropaulo"), new ClassificationInfo(category, ClassificationInfo.ResponsibleAll, Importance.Essential)),
                new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 18), -500, "Faculdade"), new ClassificationInfo("Educação", ClassificationInfo.ResponsibleAll, Importance.Essential)),
                new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 23), -800, "Aluguel"), new ClassificationInfo(category, ClassificationInfo.ResponsibleAll, Importance.Essential))
            };

            MontlyBudget budget = new MontlyBudget(category, 2_000m, 15);

            MontlyBudget processedBudget = budget.Process(lines);

            Assert.Equal(0m, budget.Used);
            Assert.Equal(2_000m, budget.Available);

            Assert.Equal(1_100m, processedBudget.Used);
            Assert.Equal(900m, processedBudget.Available);
        }
    }
}
