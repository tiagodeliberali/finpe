using Finpe.CashFlow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Finpe.Parser
{
    public class StatementParser
    {
        Regex regexAmount = new Regex(@"((\d+\.)*\d+\,\d+)", RegexOptions.IgnoreCase);
        Regex regexDescription = new Regex(@"([a-z\-]{2,}(\s[\w\.\-/]+)*)", RegexOptions.IgnoreCase);
        Regex regexDate = new Regex(@"(\d{1,2}/\d{1,2}(/\d{2,4})*)", RegexOptions.IgnoreCase);
        Regex regexDirection = new Regex(@"(\s\-\s)", RegexOptions.IgnoreCase);
        Regex regexSaldo = new Regex(@"saldo|sdo", RegexOptions.IgnoreCase);

        public List<TransactionLine> Parse(string lines)
        {
            List<TransactionLine> result = new List<TransactionLine>();

            foreach (var singleLine in lines.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                IncludeStatement(result, singleLine);
            }

            return result;
        }

        private void IncludeStatement(List<TransactionLine> result, string line)
        {
            if (!regexSaldo.Match(line).Success)
            {
                result.Add(ExtactSingleStatement(line));
            }
        }

        private ExecutedTransactionLine ExtactSingleStatement(string line)
        {
            Match matchAmount = regexAmount.Match(line);
            Match matchDescription = regexDescription.Match(line);
            Match matchDate = regexDate.Match(line);
            Match matchDirection = regexDirection.Match(line);

            string description = matchDescription.Success ? matchDescription.Value : throw new ArgumentException("line.Description");
            decimal amount = matchAmount.Success ? decimal.Parse(matchAmount.Value) : throw new ArgumentException("line.Amount");
            DateTime transactionDate = matchDate.Success ? ParseDate(matchDate.Value) : throw new ArgumentException("line.TransactionDate");

            if (matchDirection.Success)
            {
                amount *= -1;
            }

            return new ExecutedTransactionLine(transactionDate, description, amount);
        }

        private DateTime ParseDate(string value)
        {
            string dateValue = value.Length <= 5 ? value + "/" + DateTime.Today.Year : value;
            return DateTime.ParseExact(dateValue, "dd/MM/yyyy", CultureInfo.CurrentCulture);
        }
    }
}
