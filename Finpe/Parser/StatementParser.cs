using Finpe.CashFlow;
using System.Text.RegularExpressions;

namespace Finpe.Parser
{
    public class StatementParser : BaseParser
    {
        Regex regexDirection = new Regex(@"(\s\-\s)", RegexOptions.IgnoreCase);
        Regex regexSaldo = new Regex(@"saldo|sdo", RegexOptions.IgnoreCase);

        protected override bool IsNegativeAmount(string line)
        {
            Match matchDirection = regexDirection.Match(line);
            return matchDirection.Success;
        }

        protected override bool IsValidLine(string line)
        {
            return !regexSaldo.Match(line).Success;
        }

        protected override TransactionLine BuildLine(string line)
        {
            TransactionLineInfo info = ExtractTransactionLineInfo(line);
            return new ExecutedTransactionLine(info);
        }
    }
}
