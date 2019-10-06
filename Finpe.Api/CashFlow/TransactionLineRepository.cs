﻿using Finpe.Api.Utils;
using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using System.Collections.Generic;
using System.Linq;

namespace Finpe.Api.CashFlow
{
    public class TransactionLineRepository : Repository<TransactionLine>
    {
        public TransactionLineRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IReadOnlyList<TransactionLine> GetList()
        {
            return _unitOfWork
                .Query<TransactionLine>()
                .Where(x => !(x is MultilineDetailTransactionLine))
                .ToList();
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
