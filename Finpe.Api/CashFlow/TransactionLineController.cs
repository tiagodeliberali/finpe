using Finpe.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Finpe.Api.CashFlow
{
    [Route("api/[controller]")]
    public class TransactionLineController : BaseController
    {
        public TransactionLineController(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
