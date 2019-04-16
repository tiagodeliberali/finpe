using Finpe.CashFlow;
using Finpe.Parser;
using Finpe.RecurringCashFlow;
using Finpe.Utils;
using Finpe.Visualization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Finpe.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string lines = GetData();

            StatementParser parser = new StatementParser();
            List<ClassifiedTransactionLine> statements = parser.Parse(lines)
                .Select(x => (ExecutedTransactionLine)x)
                .ToList<ClassifiedTransactionLine>();

            Classificar(statements, "NET SERVIÇOS", new ClassificationInfo("Moradia", ClassificationInfo.ResponsibleAll, Importance.Essential));
            Classificar(statements, "ELETROPAULO", new ClassificationInfo("Moradia", ClassificationInfo.ResponsibleAll, Importance.Essential));
            Classificar(statements, "IPVA", new ClassificationInfo("Transporte", ClassificationInfo.ResponsibleAll, Importance.HardToCut));

            List<MonthlyView> months = new MonthlyViewBuilder(
                    statements.ToList<TransactionLine>(), 
                    new List<IViewerPipeline>() { new RecurringTransactionsPipeline(BuildRecurrences(), new YearMonth(2019, 6)) })
                .Build(-3_175.16m);

            foreach (var item in months)
            {
                Console.WriteLine(item.YearMonth.ToString());
                Console.WriteLine("Saldo inicial: " + String.Format("{0:C}", item.InitialAmount));
                foreach (var line in item.Lines)
                {
                    Console.WriteLine("\t" + string.Format("{0:dd/MM}", line.TransactionDate) +
                        "\t" + line.Description +
                        "\t" + string.Format("{0:C}", line.Amount) +
                        "\t" + ((ClassifiedTransactionLine)line).Category);
                }
                Console.WriteLine("Saldo final: " + String.Format("{0:C}", item.FinalAmount));
            }
            Console.ReadLine();
        }

        private static List<RecurringTransaction> BuildRecurrences()
        {
            return new List<RecurringTransaction>()
            {
                new RecurringTransaction("NET SERVIÇOS", -240m, 22, new ClassificationInfo("Moradia", ClassificationInfo.ResponsibleAll, Importance.Essential)) { StartYearMonth = new YearMonth(2019, 4) },
                new RecurringTransaction("ELETROPAULO", -90m, 20, new ClassificationInfo("Moradia", ClassificationInfo.ResponsibleAll, Importance.Essential)) { StartYearMonth = new YearMonth(2019, 4) }
            };
        }

        private static void Classificar(List<ClassifiedTransactionLine> statements, string searchText, ClassificationInfo classification)
        {
            foreach (ClassifiedTransactionLine line in statements.Where(x => x.Description.Contains(searchText)))
            {
                line.Classify(classification);
            }
        }

        static string GetData()
        {
            return File.ReadAllText("../../../extrato.txt");
        }
    }
}
