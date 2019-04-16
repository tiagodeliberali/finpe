using Finpe.Budget;
using Finpe.CashFlow;
using Finpe.MultilineCashflow;
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

        [Fact]
        public void GetBudgetInfoWithMultilineTransaction()
        {
            MultilineTransactionLine line = new MultilineTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 22), 0m, "Itau personalite"));
            line.Add(new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 15), -300, "farmácia"), new ClassificationInfo("saude", ClassificationInfo.ResponsibleAll, Importance.Essential)));
            line.Add(new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 15), -500, "supermercado"), new ClassificationInfo(category, ClassificationInfo.ResponsibleAll, Importance.Essential)));

            MultilineTransactionLine line2 = new MultilineTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 22), 0m, "Itau personalite"));
            line2.Add(new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 15), -200, "farmácia"), new ClassificationInfo("saude", ClassificationInfo.ResponsibleAll, Importance.Essential)));
            line2.Add(new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 15), -300, "supermercado"), new ClassificationInfo(category, ClassificationInfo.ResponsibleAll, Importance.Essential)));
            ExecutedMultilineTransactionLine executedLine = line2.Consolidate(new ExecutedTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 17), -1_000m, "PGTO CARTAO")));

            List<TransactionLine> lines = new List<TransactionLine>()
            {
                new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 14), -300m, "Eletropaulo"), new ClassificationInfo(category, ClassificationInfo.ResponsibleAll, Importance.Essential)),
                new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 18), -500, "Faculdade"), new ClassificationInfo("Educação", ClassificationInfo.ResponsibleAll, Importance.Essential)),
                line,
                executedLine,
                new SingleTransactionLine(new TransactionLineInfo(new DateTime(2019, 4, 23), -800, "Aluguel"), new ClassificationInfo(category, ClassificationInfo.ResponsibleAll, Importance.Essential))
            };

            MontlyBudget budget = new MontlyBudget(category, 2_000m, 15);

            MontlyBudget processedBudget = budget.Process(lines);

            Assert.Equal(1_900m, processedBudget.Used);
            Assert.Equal(100m, processedBudget.Available);
        }
    }
}
