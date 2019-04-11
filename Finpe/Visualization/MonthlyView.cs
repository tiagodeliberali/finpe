using Finpe.CashFlow;
using Finpe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.Visualization
{
    public class MonthlyView
    {
        private MonthlyView(int year, int month, decimal initialAmount)
        {
            Year = year;
            Month = month;
            InitialAmount = initialAmount;
            FinalAmount = initialAmount;
        }

        private List<TransactionLine> _lines = new List<TransactionLine>();
        public decimal InitialAmount { get; private set; }
        public decimal FinalAmount { get; private set; }
        public int Year { get; private set; }
        public int Month { get; private set; }
        public IReadOnlyList<TransactionLine> Lines
        {
            get
            {
                return _lines;
            }
        }

        public void Add(TransactionLine transactionLine)
        {
            _lines.Add(transactionLine);
            FinalAmount += transactionLine.Amount;
        }

        public static List<MonthlyView> Build(decimal initialAmount, List<TransactionLine> statements)
        {
            List<MonthlyView> result = new List<MonthlyView>();

            List<Tuple<int, int>> yearMonthList = GetMonthYearList(statements);

            decimal previousAmount = initialAmount;
            foreach (var yearMonth in yearMonthList)
            {
                List<TransactionLine> currentMonthLines = statements.Where(x => x.TransactionDate.Year == yearMonth.Item1 && x.TransactionDate.Month == yearMonth.Item2).ToList();
                MonthlyView month = BuildMonth(yearMonth.Item1, yearMonth.Item2, previousAmount, currentMonthLines);
                result.Add(month);
                previousAmount = month.FinalAmount;
            }


            return result;
        }

        private static List<Tuple<int, int>> GetMonthYearList(List<TransactionLine> statements)
        {
            DateTime minDate = statements.OrderBy(x => x.TransactionDate).First().TransactionDate.FirstDay();
            DateTime maxDate = statements.OrderBy(x => x.TransactionDate).Last().TransactionDate.LastDay();

            List<Tuple<int, int>> results = new List<Tuple<int, int>>();

            for (DateTime i = minDate; i < maxDate; i = i.AddMonths(1))
            {
                results.Add(new Tuple<int, int>(i.Year, i.Month));
            }

            return results;
        }

        private static MonthlyView BuildMonth(int year, int month, decimal initialAmount, List<TransactionLine> statements)
        {
            MonthlyView monthlyStatement = new MonthlyView(
                            year,
                            month,
                            initialAmount);

            foreach (var item in statements)
            {
                monthlyStatement.Add(item);
            }

            return monthlyStatement;
        }
    }
}
