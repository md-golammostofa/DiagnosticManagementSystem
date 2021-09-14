namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblSpecialistsOrgAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblSpecialists", "OrgId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblSpecialists", "OrgId");
        }
    }
}
