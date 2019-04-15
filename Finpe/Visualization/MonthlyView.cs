using Finpe.CashFlow;
using Finpe.RecurringCashFlow;
using Finpe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.Visualization
{
    public class MonthlyView
    {
        private MonthlyView(YearMonth yearMonth, decimal initialAmount)
        {
            YearMonth = yearMonth;
            InitialAmount = initialAmount;
            FinalAmount = initialAmount;
        }

        private List<TransactionLine> _lines = new List<TransactionLine>();
        public decimal InitialAmount { get; private set; }
        public decimal FinalAmount { get; private set; }
        public YearMonth YearMonth { get; private set; }
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

            List<YearMonth> yearMonthList = GetMonthYearList(statements);

            decimal previousAmount = initialAmount;
            foreach (var yearMonth in yearMonthList)
            {
                List<TransactionLine> currentMonthLines = statements.Where(x => yearMonth.Equals(x.TransactionDate)).ToList();
                MonthlyView month = BuildMonth(yearMonth, previousAmount, currentMonthLines);
                result.Add(month);
                previousAmount = month.FinalAmount;
            }


            return result;
        }

        public static List<MonthlyView> Build(decimal initialAmount, List<TransactionLine> statements, List<RecurringTransaction> recurringTransactions, YearMonth finalYearMonth)
        {
            YearMonth initialYearMonth = statements.Select(x => x.TransactionDate).Min().ToYearMonth();

            foreach (var recurringTransaction in recurringTransactions)
            {
                recurringTransaction.IncludeLines(statements, initialYearMonth, finalYearMonth);
            }

            return Build(initialAmount, statements);
        }

        private static List<YearMonth> GetMonthYearList(List<TransactionLine> statements)
        {
            DateTime minDate = statements.OrderBy(x => x.TransactionDate).First().TransactionDate.FirstDay();
            DateTime maxDate = statements.OrderBy(x => x.TransactionDate).Last().TransactionDate.LastDay();

            List<YearMonth> results = new List<YearMonth>();

            for (DateTime i = minDate; i < maxDate; i = i.AddMonths(1))
            {
                results.Add(i.ToYearMonth());
            }

            return results;
        }

        private static MonthlyView BuildMonth(YearMonth yearMonth, decimal initialAmount, List<TransactionLine> statements)
        {
            MonthlyView monthlyStatement = new MonthlyView(yearMonth, initialAmount);

            foreach (var item in statements)
            {
                monthlyStatement.Add(item);
            }

            return monthlyStatement;
        }
    }
}
