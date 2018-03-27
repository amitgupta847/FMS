namespace FMS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notnull : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BankID = c.Int(nullable: false),
                        AccountNumber = c.String(nullable: false, maxLength: 25),
                        OwnerName = c.String(maxLength: 50),
                        User_Id = c.String(maxLength: 50),
                        Pwd_1 = c.String(maxLength: 50),
                        Pwd_2 = c.String(maxLength: 50),
                        Date_Acc_Open = c.DateTime(nullable: false),
                        IsAccActive = c.Boolean(nullable: false),
                        IsJointlyOwned = c.Boolean(nullable: false),
                        Comment = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Bank", t => t.BankID, cascadeDelete: true)
                .Index(t => t.BankID);
            
            CreateTable(
                "dbo.Bank",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 250),
                        IFSC = c.String(nullable: false, maxLength: 30),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 50),
                        Comment = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Deposit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DepositAccNumber = c.String(nullable: false, maxLength: 50),
                        YearID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        DepositDate = c.DateTime(nullable: false),
                        Interest = c.Single(nullable: false),
                        MaturityDate = c.DateTime(nullable: false),
                        InitialDeposit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaturityValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.YearLine", t => t.YearID, cascadeDelete: true)
                .Index(t => t.YearID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.YearLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        NumberOfDeposits = c.Int(nullable: false),
                        YearTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaturityTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deposit", "YearID", "dbo.YearLine");
            DropForeignKey("dbo.Deposit", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Account", "BankID", "dbo.Bank");
            DropIndex("dbo.Deposit", new[] { "AccountID" });
            DropIndex("dbo.Deposit", new[] { "YearID" });
            DropIndex("dbo.Account", new[] { "BankID" });
            DropTable("dbo.YearLine");
            DropTable("dbo.Deposit");
            DropTable("dbo.Bank");
            DropTable("dbo.Account");
        }
    }
}
