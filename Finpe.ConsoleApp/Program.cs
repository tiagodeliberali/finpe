using Finpe.CashFlow;
using Finpe.Parser;
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

            Classificar(statements, "NET SERVIÇOS", "Moradia", "Todos", Importance.Essential);
            Classificar(statements, "ELETROPAULO", "Moradia", "Todos", Importance.Essential);
            Classificar(statements, "IPVA", "Transporte", "Todos", Importance.HardToCut);

            List<MonthlyView> months = MonthlyView.Build(-3_175.16m, statements.ToList<TransactionLine>());

            foreach (var item in months)
            {
                Console.WriteLine(item.Month + "/" + item.Year);
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

        private static void Classificar(List<ClassifiedTransactionLine> statements, string searchText, string category, string responsible, Importance importance)
        {
            foreach (ClassifiedTransactionLine line in statements.Where(x => x.Description.Contains(searchText)))
            {
                line.Classify(category, responsible, importance);
            }
        }

        static string GetData()
        {
            return File.ReadAllText("../../../extrato.txt");
        }
    }
}
