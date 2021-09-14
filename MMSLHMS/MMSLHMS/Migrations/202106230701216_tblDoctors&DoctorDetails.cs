namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblDoctorsDoctorDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblDoctorDetails",
                c => new
                    {
                        TDId = c.Long(nullable: false, identity: true),
                        Day = c.String(),
                        DayOfTime = c.String(),
                        StartTime = c.String(),
                        EndTime = c.String(),
                        EntryUser = c.String(),
                        OrgId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        DocName = c.String(),
                        DocSpecialist = c.String(),
                        GenerelFee = c.Double(),
                        DocDegree = c.String(),
                        DocMobile = c.String(),
                        DocId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TDId)
                .ForeignKey("dbo.tblDoctors", t => t.DocId, cascadeDelete: true)
                .Index(t => t.DocId);
            
            CreateTable(
                "dbo.tblDoctors",
                c => new
                    {
                        DocId = c.Long(nullable: false, identity: true),
                        DocName = c.String(),
                        DocDegree = c.String(),
                        DocRegNo = c.String(),
                        DocAddress = c.String(),
                        DocType = c.String(),
                        DocSpecialist = c.String(),
                        DocMobile = c.String(),
                        DocEmail = c.String(),
                        HospitalName = c.String(),
                        Remarks = c.String(),
                        GenerelFee = c.Double(),
                        ReportFee = c.Double(),
                        FollowUpFee = c.Double(),
                        CounselingFee = c.Double(nullable: false),
                        OtherProfession = c.String(),
                        Sex = c.String(),
                        DOB = c.DateTime(nullable: false),
                        DocImage = c.String(),
                        DocSign = c.String(),
                        EntryUser = c.String(),
                        OrgId = c.Long(),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.DocId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblDoctorDetails", "DocId", "dbo.tblDoctors");
            DropIndex("dbo.tblDoctorDetails", new[] { "DocId" });
            DropTable("dbo.tblDoctors");
            DropTable("dbo.tblDoctorDetails");
        }
    }
}
