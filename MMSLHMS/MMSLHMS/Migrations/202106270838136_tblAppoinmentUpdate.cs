namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblAppoinmentUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblAppoinments", "DoctorName", c => c.String());
            DropColumn("dbo.tblAppoinments", "DoctorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblAppoinments", "DoctorId", c => c.Long());
            DropColumn("dbo.tblAppoinments", "DoctorName");
        }
    }
}
