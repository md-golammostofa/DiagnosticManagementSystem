namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefFeeaddInvestigation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblInvestigations", "RefFee", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblInvestigations", "RefFee");
        }
    }
}
