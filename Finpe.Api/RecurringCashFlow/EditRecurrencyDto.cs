namespace Finpe.Api.RecurringCashFlow
{
    public class EditRecurrencyDto
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
