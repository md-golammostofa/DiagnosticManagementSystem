namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblAppoinment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblAppoinments",
                c => new
                    {
                        AppId = c.Long(nullable: false, identity: true),
                        DoctorId = c.Long(),
                        PatientName = c.String(),
                        PatientAddress = c.String(),
                        PatientMobile = c.String(),
                        SateStatus = c.String(),
                        Time = c.String(),
                        Shift = c.String(),
                        AppoinmentDate = c.DateTime(),
                        EntryUser = c.String(),
                        OrgId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.AppId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblAppoinments");
        }
    }
}
