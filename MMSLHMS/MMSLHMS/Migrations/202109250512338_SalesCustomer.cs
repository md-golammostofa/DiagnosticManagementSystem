namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblSalesCustomer",
                c => new
                    {
                        CustomerId = c.Long(nullable: false, identity: true),
                        CustomerName = c.String(),
                        CustomerAddress = c.String(),
                        CustomerMobile = c.String(),
                        CustomerEmail = c.String(),
                        Remarks = c.String(),
                        OrgId = c.Long(),
                        EntryUser = c.String(),
                        EntryDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblSalesCustomer");
        }
    }
}
