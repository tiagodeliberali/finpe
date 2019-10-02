using System;
using Finpe.CashFlow;

namespace Finpe.Api.CashFlow
{
    public class TransactionDto
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Responsible { get; set; }
        public Importance Importance { get; set; }
        public bool IsMultiline { get; set; }
        public long? MultilineParentId { get; set; }
    }
}
