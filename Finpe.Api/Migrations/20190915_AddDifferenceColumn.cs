using FluentMigrator;

namespace Finpe.Api.Migrations
{
    [Migration(20190915192600)]
    public class AddDifferenceColumn : Migration
    {
        public override void Up()
        {
            Alter.Table("TransactionLine")
                .AddColumn("Difference").AsDecimal();
        }

        public override void Down()
        {
            Delete.Column("Difference").FromTable("TransactionLine");
        }
    }
}
