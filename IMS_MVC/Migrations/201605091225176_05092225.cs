namespace IMS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05092225 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IntInfoes", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IntInfoes", "Status", c => c.Int(nullable: false));
        }
    }
}
