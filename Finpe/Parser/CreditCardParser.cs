using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Finpe.CashFlow;
using Finpe.MultilineCashflow;

namespace Finpe.Parser
{
    public class CreditCardParser : BaseParser
    {
        Regex regexResponsible = new Regex(@"([a-z]{2,}(\s[\w\.\-/]+)*(\s\(\d+\)))", RegexOptions.IgnoreCase);
        private string currentResponsible = "";

        protected override bool IsNegativeAmount(string line)
        {
            return true;
        }

        protected override bool IsValidLine(string line)
        {
            Match responsibleMatch = regexResponsible.Match(line);

            if (responsibleMatch.Success)
            {
                currentResponsible = responsibleMatch.Value.Split(" ")[0];
                return false;
            }

            return true;
        }

        protected override TransactionLine BuildLine(string line)
        {
            TransactionLineInfo info = ExtractTransactionLineInfo(line);
            return new MultilineDetailTransactionLine(info, new ClassificationInfo("", currentResponsible, Importance.NotDefined));
        }

        public MultilineTransactionLine ParseName(string fileName)
        {
            Regex regexDescription = new Regex(@"([a-z]{2,}(\s[a-z\.\-/]+)*)", RegexOptions.IgnoreCase);
            Regex regexDate = new Regex(@"(\d{2}\-\d{2}(\-\d{2,4})*)", RegexOptions.IgnoreCase);

            Match matchDescription = regexDescription.Match(fileName);
            Match matchDate = regexDate.Match(fileName);

            string description = matchDescription.Success ? matchDescription.Value : throw new ArgumentException("fileName.Description");
            DateTime transactionDate = matchDate.Success ? ParseDate(matchDate) : throw new ArgumentException("fileName.TransactionDate");

            return new MultilineTransactionLine(new TransactionLineInfo(transactionDate, 0m, description));
        }

        private static DateTime ParseDate(Match matchDate)
        {
            try
            {
                return DateTime.ParseExact(matchDate.Value, "dd-MM-yy", CultureInfo.CurrentCulture);
            }
            catch(Exception)
            {
                throw new ArgumentException("fileName.TransactionDate");
            }            
        }
    }
}
