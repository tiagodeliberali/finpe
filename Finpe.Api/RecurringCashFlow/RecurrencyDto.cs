using Finpe.CashFlow;
using System;

namespace Finpe.Api.RecurringCashFlow
{
    public class RecurrencyDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime? EndDate { get; set; }
        public string Responsible { get; set; }
        public Importance Importance { get; set; }
        public string Category { get; set; }
    }
}
