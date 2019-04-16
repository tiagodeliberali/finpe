using Finpe.Budget;
using Finpe.CashFlow;
using Finpe.Utils;
using System.Collections.Generic;

namespace Finpe.Visualization
{
    public class MonthlyView
    {
        public MonthlyView(YearMonth yearMonth, decimal initialAmount)
        {
            YearMonth = yearMonth;
            InitialAmount = initialAmount;
            FinalAmount = initialAmount;
        }

        private List<TransactionLine> _lines = new List<TransactionLine>();
        public decimal InitialAmount { get; private set; }
        public decimal FinalAmount { get; private set; }
        public YearMonth YearMonth { get; private set; }
        public List<MontlyBudget> Budgets { get; internal set; }

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
    }
}
