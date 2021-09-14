namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblSpecialists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblSpecialists",
                c => new
                    {
                        SpId = c.Long(nullable: false, identity: true),
                        Specialization = c.String(maxLength: 100),
                        Remarks = c.String(),
                        EntryUser = c.String(maxLength: 50),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.SpId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblSpecialists");
        }
    }
}
