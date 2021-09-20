namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockInfoDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblProductStockDetails",
                c => new
                    {
                        PSDetailsId = c.Long(nullable: false, identity: true),
                        Category = c.String(),
                        ProductName = c.String(),
                        StockStatus = c.String(),
                        Quantity = c.Int(nullable: false),
                        CostPrice = c.Double(nullable: false),
                        SellPrice = c.Double(nullable: false),
                        Remarks = c.String(),
                        OrgId = c.Long(),
                        EntryUser = c.String(maxLength: 50),
                        EntryDate = c.DateTime(),
                        UpdateUser = c.String(maxLength: 50),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PSDetailsId);
            
            CreateTable(
                "dbo.tblProductStockInfo",
                c => new
                    {
                        PSInfoId = c.Long(nullable: false, identity: true),
                        Category = c.String(),
                        ProductName = c.String(),
                        StockInQty = c.Int(nullable: false),
                        StockOutQty = c.Int(nullable: false),
                        CostPrice = c.Double(nullable: false),
                        SellPrice = c.Double(nullable: false),
                        Remarks = c.String(),
                        OrgId = c.Long(),
                        EntryUser = c.String(maxLength: 50),
                        EntryDate = c.DateTime(),
                        UpdateUser = c.String(maxLength: 50),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PSInfoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblProductStockInfo");
            DropTable("dbo.tblProductStockDetails");
        }
    }
}
