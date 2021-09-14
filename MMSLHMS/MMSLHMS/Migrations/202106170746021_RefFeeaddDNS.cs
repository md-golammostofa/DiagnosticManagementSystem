namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefFeeaddDNS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblDiagnosticBillDetails", "RefFee", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.tblDiagnosticBillInfo", "TotalRefFee", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblDiagnosticBillInfo", "TotalRefFee");
            DropColumn("dbo.tblDiagnosticBillDetails", "RefFee");
        }
    }
}
