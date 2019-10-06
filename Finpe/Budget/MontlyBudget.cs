using System;
using System.Collections.Generic;
using System.Linq;
using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using Finpe.Utils;

namespace Finpe.Budget
{
    public class MontlyBudget : Entity
    {
        public virtual int ExecutionDay { get; set; }
        public virtual string Category { get; set; }

        public virtual decimal Used { get; protected set; }
        public virtual decimal Available { get; set; }

        public MontlyBudget(string category, decimal amount, int executionDay)
        {
            Category = category;
            Available = amount;
            ExecutionDay = executionDay;
        }

        private MontlyBudget(string category, decimal amount, int executionDay, decimal used)
        {
            Category = category;
            ExecutionDay = executionDay;
            Available = amount - used;
            Used = used;
        }

        public virtual MontlyBudget Process(List<TransactionLine> lines)
        {
            decimal used = ProcessClassifiedLines(lines);
            
            return new MontlyBudget(Category, Available, ExecutionDay, used);
        }

        protected MontlyBudget()
        {
        }

        private decimal ProcessClassifiedLines(List<TransactionLine> lines)
        {
            return Math.Abs(lines
                            .Where(x => x is ClassifiedTransactionLine)
                            .Select(x => (ClassifiedTransactionLine)x)
                            .Where(x => x.Category == Category)
                            .Sum(x => x.Amount));
        }

        public virtual void IncludeLine(List<TransactionLine> statements, YearMonth yearMonth)
        {
            if (Available > 0)
            {
                statements.Add(
                    new BudgetTransactionLine(BuildTransactionLineInfo(yearMonth), BuildClassificationInfo()));
            }
        }

        private TransactionLineInfo BuildTransactionLineInfo(YearMonth yearMonth)
        {
            return new TransactionLineInfo(yearMonth.ToDate(ExecutionDay), -Available, "Budget - " + Category);
        }

        private ClassificationInfo BuildClassificationInfo()
        {
            return new ClassificationInfo(Category, ClassificationInfo.ResponsibleAll, Importance.CanBeCut);
        }
    }
}
