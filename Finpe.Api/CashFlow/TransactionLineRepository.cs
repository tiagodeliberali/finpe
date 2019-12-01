using Finpe.Api.Utils;
using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.Api.CashFlow
{
    public class TransactionLineRepository : Repository<TransactionLine>
    {
        public TransactionLineRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IReadOnlyList<TransactionLine> GetList(DateTime currentDate, DateTime endDate)
        {
            return _unitOfWork
                .Query<TransactionLine>()
                .Where(x => !(x is MultilineDetailTransactionLine))
                .Where(x => x.TransactionDate >= StartOfTheMonth(currentDate)
                    && x.TransactionDate <= EndOfTheMonth(endDate))
                .ToList();
        }

        private DateTime StartOfTheMonth(DateTime currentDate)
        {
            return new DateTime(currentDate.Year, currentDate.Month, 1);
        }

        private DateTime EndOfTheMonth(DateTime currentDate)
        {
            return new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1).AddDays(-1);
        }

        public IReadOnlyList<TransactionLine> GetMultilineList()
        {
            return _unitOfWork
                .Query<TransactionLine>()
                .Where(x => x is MultilineTransactionLine)
                .ToList();
        }

        public IReadOnlyList<MultilineDetailTransactionLine> GetMultilineDetailTransactionLine(long parentId)
        {
            return _unitOfWork
                .Query<TransactionLine>()
                .Where(x => x is MultilineTransactionLine && ((MultilineTransactionLine)x).Id == parentId)
                .SelectMany(x => ((MultilineTransactionLine)x).Lines)
                .ToList();
        }
    }
}
