using Finpe.CashFlow;
using Finpe.MultilineCashflow;
using Finpe.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Finpe.Test
{
    public class CreditCardParserTest
    {
        CreditCardParser parser = new CreditCardParser();

        [Fact]
        public void ImportSingleStatement()
        {
            string line = "24/05	FAST SHOP 10/10	156,39";

            List<TransactionLine> statements = parser.Parse(line);

            Assert.Single(statements);

            MultilineDetailTransactionLine transactionLine = statements.First() as MultilineDetailTransactionLine;
            ValidateStatement(transactionLine, -156.39m, "FAST SHOP 10/10", DateTime.Parse("2019-05-24"));
        }

        [Fact]
        public void ImportPositiveSingleStatement()
        {
            string line = "28/02	DESCONTO ANUIDADE 04/12	-19,95";

            List<TransactionLine> statements = parser.Parse(line);

            Assert.Single(statements);

            MultilineDetailTransactionLine transactionLine = statements.First() as MultilineDetailTransactionLine;
            Assert.Equal(19.95m, transactionLine.Amount);
            Assert.Equal("DESCONTO ANUIDADE 04/12", transactionLine.Description);
            Assert.Equal(DateTime.Parse("2019-02-28"), transactionLine.TransactionDate);
        }

        [Fact]
        public void AssociateWithResponsible()
        {
            string lines = @"GLAUCILENE DIAS O SANTOS (9420)
                            28/02	DESCONTO ANUIDADE 04/12	-19,95";

            List<TransactionLine> statements = parser.Parse(lines);

            ValidateStatement(statements[0], 19.95m, "DESCONTO ANUIDADE 04/12", DateTime.Parse("2019-02-28"), "GLAUCILENE");
        }

        [Fact]
        public void ImportSetOfStatements()
        {
            string lines = @"Lançamentos nacionais
                            GLAUCILENE DIAS O SANTOS (9420)
                            DATA	MOVIMENTAÇÃO	VALOR EM R$
                            28/02	DESCONTO ANUIDADE 04/12	-19,95
                            28/02	PARCELA DE ANUIDAD04/12	19,95
                            Crédito do cartão final (9420)	-19,95
                            Débito do cartão final (9420)	19,95

                            Lançamentos nacionais
                            TIAGO DELIBERALI (5418)
                            DATA	MOVIMENTAÇÃO	VALOR EM R$
                            17/10	PAYPAL*PONTOFRIO 05/10	312,12
                            26/11	TROCAFONE 04/12	62,49
                            Crédito do cartão final (5418)	0,00
                            Débito do cartão final (5418)	374,61";

            List<TransactionLine> statements = parser.Parse(lines);

            Assert.Equal(4, statements.Count);

            ValidateStatement(statements[0], 19.95m, "DESCONTO ANUIDADE 04/12", DateTime.Parse("2019-02-28"), "GLAUCILENE");
            ValidateStatement(statements[1], -19.95m, "PARCELA DE ANUIDAD04/12", DateTime.Parse("2019-02-28"), "GLAUCILENE");
            ValidateStatement(statements[2], -312.12m, "PAYPAL*PONTOFRIO 05/10", DateTime.Parse("2019-10-17"), "TIAGO");
            ValidateStatement(statements[3], -62.49m, "TROCAFONE 04/12", DateTime.Parse("2019-11-26"), "TIAGO");
        }

        [Theory]
        [InlineData("Cash Back 20-04-19", 20, 4, "Cash Back")]
        [InlineData("Nubank 17-05-19", 17, 5, "Nubank")]
        public void ParseNameToMultilineTransactionLine(string name, int day, int month, string description)
        {
            MultilineTransactionLine transactionLine = parser.ParseName(name);

            ValidateStatement(transactionLine, 0m, description, new DateTime(2019, month, day));
        }

        [Theory]
        [InlineData("Cash Back 20/04/19")]
        [InlineData("Nubank 17-5-19")]
        [InlineData("Nubank 17-05-2019")]
        [InlineData("Nubank")]
        public void ShouldThrowExceptionOnInvalidName(string name)
        {
            Assert.Throws<ArgumentException>(() => parser.ParseName(name));
        }

        private static void ValidateStatement(TransactionLine transactionLine, decimal amount, string description, DateTime date, string responsible = "")
        {
            Assert.Equal(amount, transactionLine.Amount);
            Assert.Equal(description, transactionLine.Description);
            Assert.Equal(date, transactionLine.TransactionDate);

            if (transactionLine is ClassifiedTransactionLine)
            {
                Assert.Equal(responsible, ((ClassifiedTransactionLine)transactionLine).Responsible);
            }
        }
    }
}
