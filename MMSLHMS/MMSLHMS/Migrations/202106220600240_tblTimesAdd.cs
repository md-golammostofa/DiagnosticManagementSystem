namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblTimesAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblTimes",
                c => new
                    {
                        TimeId = c.Long(nullable: false, identity: true),
                        Time = c.String(),
                        ScendTimeF = c.String(),
                        EntryUser = c.String(),
                        OrgId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TimeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblTimes");
        }
    }
}
