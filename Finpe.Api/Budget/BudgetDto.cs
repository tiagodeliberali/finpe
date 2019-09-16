using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finpe.Api.Budget
{
    public class BudgetDto
    {
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public int Day { get; set; }
    }
}
