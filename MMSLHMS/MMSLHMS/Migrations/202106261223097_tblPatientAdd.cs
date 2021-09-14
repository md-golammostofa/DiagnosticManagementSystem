namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblPatientAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblPatients",
                c => new
                    {
                        PatientId = c.Long(nullable: false, identity: true),
                        PatiendName = c.String(),
                        PatientAddress = c.String(),
                        PatientMobile = c.String(),
                        PatientSex = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        PatientEmail = c.String(),
                        Remarks = c.String(),
                        PatientWeight = c.String(),
                        EntryUser = c.String(),
                        OrgId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblPatients");
        }
    }
}
