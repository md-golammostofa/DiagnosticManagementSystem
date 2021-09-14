namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblAppoinmentAge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblAppoinments", "Age", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblAppoinments", "Age");
        }
    }
}
