namespace IMS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05252012 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IntInfoes", "AspNetUserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IntInfoes", "AspNetUserId", c => c.String(nullable: false));
        }
    }
}
