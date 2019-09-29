namespace Finpe.Api.Budget
{
    public class BudgetDto
    {
        public long Id { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public int Day { get; set; }
    }
}
