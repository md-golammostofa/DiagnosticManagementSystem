namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblOtherProfession : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblOtherProfessional",
                c => new
                    {
                        OPId = c.Long(nullable: false, identity: true),
                        Profession = c.String(),
                        Remarks = c.String(),
                        EntryUser = c.String(maxLength: 50),
                        OrgId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.OPId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblOtherProfessional");
        }
    }
}
