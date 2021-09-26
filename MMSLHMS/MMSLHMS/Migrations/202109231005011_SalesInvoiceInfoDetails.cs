namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesInvoiceInfoDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblSalesInvoiceDetails",
                c => new
                    {
                        IDetailsId = c.Long(nullable: false, identity: true),
                        CategoryName = c.String(),
                        ProductName = c.String(),
                        SellPrice = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Total = c.Double(nullable: false),
                        Remarks = c.String(),
                        OrgId = c.Long(),
                        EntryUser = c.String(),
                        EntryDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IInfoId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.IDetailsId)
                .ForeignKey("dbo.tblSalesInvoiceInfo", t => t.IInfoId, cascadeDelete: true)
                .Index(t => t.IInfoId);
            
            CreateTable(
                "dbo.tblSalesInvoiceInfo",
                c => new
                    {
                        IInfoId = c.Long(nullable: false, identity: true),
                        InvoiceNo = c.String(),
                        CustomerType = c.String(),
                        CustomerName = c.String(),
                        CustomerMobile = c.String(),
                        CustomerAddress = c.String(),
                        PaymentMethod = c.String(),
                        PaymentStatus = c.String(),
                        SubTotalAmount = c.Double(nullable: false),
                        VATAmount = c.Double(nullable: false),
                        TAXAmount = c.Double(nullable: false),
                        DisCountAmount = c.Double(nullable: false),
                        NetAmount = c.Double(nullable: false),
                        ReceiveAmount = c.Double(nullable: false),
                        DueAmount = c.Double(nullable: false),
                        Remarks = c.String(),
                        OrgId = c.Long(),
                        EntryUser = c.String(maxLength: 50),
                        EntryDate = c.DateTime(),
                        UpdateUser = c.String(maxLength: 50),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.IInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblSalesInvoiceDetails", "IInfoId", "dbo.tblSalesInvoiceInfo");
            DropIndex("dbo.tblSalesInvoiceDetails", new[] { "IInfoId" });
            DropTable("dbo.tblSalesInvoiceInfo");
            DropTable("dbo.tblSalesInvoiceDetails");
        }
    }
}
