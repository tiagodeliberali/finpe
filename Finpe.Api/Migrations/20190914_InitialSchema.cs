using FluentMigrator;
using System;

namespace Finpe.Api.Migrations
{
    [Migration(20180430121800)]
    public class InitialSchema : Migration
    {
        public override void Up()
        {
            Create.Table("MontlyBudget")
                .WithColumn("MontlyBudgetID").AsInt64().PrimaryKey().Identity()
                .WithColumn("ExecutionDay").AsInt32().NotNullable()
                .WithColumn("Category").AsString(255).NotNullable()
                .WithColumn("Available").AsDecimal().NotNullable();

            Create.Table("RecurringTransaction")
                .WithColumn("RecurringTransactionID").AsInt64().PrimaryKey().Identity()
                .WithColumn("Day").AsInt32().NotNullable()
                .WithColumn("Description").AsString(255).NotNullable()
                .WithColumn("Amount").AsDecimal().NotNullable()
                .WithColumn("StartYearMonth_Month").AsInt32().NotNullable()
                .WithColumn("StartYearMonth_Year").AsInt32().NotNullable()
                .WithColumn("EndYearMonth_Month").AsInt32().Nullable()
                .WithColumn("EndYearMonth_Year").AsInt32().Nullable()
                .WithColumn("Category").AsString(255).NotNullable()
                .WithColumn("Responsible").AsString(255).NotNullable()
                .WithColumn("Importance").AsString(255).NotNullable();

            Create.Table("TransactionLine")
                .WithColumn("TransactionLineID").AsInt64().PrimaryKey().Identity()
                .WithColumn("TransactionLineType").AsString(255).NotNullable()
                .WithColumn("TransactionDate").AsDateTime2().NotNullable()
                .WithColumn("Amount").AsDecimal().NotNullable()
                .WithColumn("Description").AsString(255).NotNullable()
                .WithColumn("Category").AsString(255).Nullable()
                .WithColumn("Responsible").AsString(255).Nullable()
                .WithColumn("Importance").AsString(255).Nullable()
                .WithColumn("MultiCategoryTransactionLineID").AsInt64().Nullable()
                .WithColumn("Difference").AsDecimal().Nullable();

            Create.ForeignKey("FK_MultiCategoryTransactionLine_TransactionLine")
                .FromTable("TransactionLine").ForeignColumn("MultiCategoryTransactionLineID")
                .ToTable("TransactionLine").PrimaryColumn("TransactionLineID");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_MultiCategoryTransactionLine_TransactionLine");
            Delete.Table("MontlyBudget");
            Delete.Table("RecurringTransaction");
            Delete.Table("TransactionLine");
        }        
    }
}
