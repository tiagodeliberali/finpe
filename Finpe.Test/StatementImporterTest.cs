using Finpe.Parser;
using Finpe.Statement;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Finpe.Test
{
    public class StatementImporterTest
    {
        [Fact]
        public void ImportSingleStatement()
        {
            string line = "18/03	 D		INT PAG TIT BANCO 237        		9.952,27	-	";

            StatementParser parser = new StatementParser();
            List<StatementLine> statements = parser.Parse(line);

            Assert.Single(statements);
            ValidateStatement<OutcomeStatementLine>(statements.First(), 9_952.27m, "INT PAG TIT BANCO 237", DateTime.Parse("2019-03-18"));
        }

        [Theory]
        [InlineData("18	 D		INT PAG TIT BANCO 237        		952,27	-	")]
        [InlineData("18/03	 D		237        		952,27	-	")]
        [InlineData("18/03	 D		INT PAG TIT BANCO 237        		95227	-	")]
        public void ImportInvalidLine(String line)
        {
            StatementParser parser = new StatementParser();

            Assert.Throws<ArgumentException>(() => parser.Parse(line));
        }

        [Fact]
        public void ImportStatementWithCompleteDate()
        {
            string line = "18/03/2018	 D		INT PAG TIT BANCO 237        		952,27	-	";

            StatementParser parser = new StatementParser();
            StatementLine statement = parser.Parse(line)
                                            .First();

            ValidateStatement<OutcomeStatementLine>(statement, date: DateTime.Parse("2018-03-18"));
        }

        [Fact]
        public void ImportPositiveStatementAsIncomeStatement()
        {
            string line = "29/03			REMUNERACAO/SALARIO       	1370	4.730,81		";

            StatementParser parser = new StatementParser();
            StatementLine statement = parser.Parse(line)
                                            .First();

            Assert.IsType<IncomeStatementLine>(statement);
        }

        [Fact]
        public void ImportNegativeStatementAsOutcomeStatement()
        {
            string line = "18/03	 D		INT PAG TIT BANCO 237        		9.952,27	-	";

            StatementParser parser = new StatementParser();
            StatementLine statement = parser.Parse(line)
                                            .First();

            Assert.IsType<OutcomeStatementLine>(statement);
        }

        [Fact]
        public void ShouldIgnoreSaldo()
        {
            string line = "04/04			SALDO DO DIA       				1.966,31	-";

            StatementParser parser = new StatementParser();
            List<StatementLine> statements = parser.Parse(line);

            Assert.Empty(statements);
        }

        [Fact]
        public void ImportSetOfStatements()
        {
            string lines = @"22/03			SALDO DO DIA       				1.951,23	-
                            25/03			TIT PAG TIT ULO ITAU       		630,10	-		
                            25/03			TBI 0435.67680-4TRANSFER       		1.000,00	-		
                            25/03			TBI 0641.05595-9UNICAMP       		110,00			
                            25/03			RSHOP-AUTOPASS -05/04       	7071	109,27	-	";

            StatementParser parser = new StatementParser();
            List<StatementLine> statements = parser.Parse(lines);

            Assert.Equal(4, statements.Count);

            ValidateStatement<OutcomeStatementLine>(statements[0], 630.10m, "TIT PAG TIT ULO ITAU", DateTime.Parse("2019-03-25"));
            ValidateStatement<OutcomeStatementLine>(statements[1], 1_000m, "TBI 0435.67680-4TRANSFER", DateTime.Parse("2019-03-25"));
            ValidateStatement<IncomeStatementLine>(statements[2], 110m, "TBI 0641.05595-9UNICAMP", DateTime.Parse("2019-03-25"));
            ValidateStatement<OutcomeStatementLine>(statements[3], 109.27m, "RSHOP-AUTOPASS -05/04", DateTime.Parse("2019-03-25"));
        }

        private static void ValidateStatement<T>(StatementLine statement, decimal? amount = null, string description = null, DateTime? date = null)
        {
            Assert.IsType<T>(statement);
            if (amount.HasValue) Assert.Equal(amount, statement.Amount);
            if (description != null) Assert.Equal(description, statement.Description);
            if (date.HasValue) Assert.Equal(date, statement.TransactionDate);
        }
    }
}
