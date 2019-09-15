using Finpe.CashFlow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Finpe.Parser
{
    public abstract class BaseParser
    {
        Regex regexAmount = new Regex(@"(\-?(\d+\.)*\d+\,\d+)", RegexOptions.IgnoreCase);
        Regex regexDescription = new Regex(@"([a-z\-\*]{2,}(\s[\w\*\.\-/]+)*)", RegexOptions.IgnoreCase);
        Regex regexDate = new Regex(@"(\d{1,2}/\d{1,2}(/\d{2,4})*)", RegexOptions.IgnoreCase);
        
        protected abstract bool IsNegativeAmount(string line);

        protected abstract bool IsValidLine(string line);

        protected abstract TransactionLine BuildLine(string line);

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
            if (IsValidLine(line))
            {
                try
                {
                    result.Add(BuildLine(line));
                }
                catch(Exception)
                {
                    // do nothing...
                }
            }
        }

        protected TransactionLineInfo ExtractTransactionLineInfo(string line)
        {
            LineProcessor processor = new LineProcessor(line);
            Match matchAmount = processor.MatchAndRemove(regexAmount);
            Match matchDate = processor.MatchAndRemove(regexDate);
            Match matchDescription = processor.MatchAndRemove(regexDescription);            
            
            string description = matchDescription.Success ? matchDescription.Value : throw new ArgumentException("line.Description");
            decimal amount = matchAmount.Success ? decimal.Parse(matchAmount.Value, new CultureInfo("pt-BR")) : throw new ArgumentException("line.Amount");
            DateTime transactionDate = matchDate.Success ? ParseDate(matchDate.Value) : throw new ArgumentException("line.TransactionDate");

            if (IsNegativeAmount(line))
            {
                amount *= -1;
            }

            return new TransactionLineInfo(transactionDate, amount, description);
        }

        private DateTime ParseDate(string value)
        {
            string dateValue = value.Length <= 5 ? value + "/" + DateTime.Today.Year : value;
            return DateTime.ParseExact(dateValue, "dd/MM/yyyy", CultureInfo.CurrentCulture);
        }

        class LineProcessor
        {
            private string line;

            public LineProcessor(string line)
            {
                this.line = line;
            }

            public Match MatchAndRemove(Regex expression)
            {
                Match match = expression.Match(line);

                if (match.Success)
                {
                    var regex = new Regex(Regex.Escape(match.Value));
                    line = regex.Replace(line, "", 1);
                }

                return match;
            }
        }
    }
}
