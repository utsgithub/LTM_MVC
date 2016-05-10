namespace IMS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05101715 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IntInfoes", "AspNetUserId", c => c.String(nullable: false));
            DropColumn("dbo.IntInfoes", "UserId");
            DropColumn("dbo.Users", "UserType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserType", c => c.String(nullable: false));
            AddColumn("dbo.IntInfoes", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.IntInfoes", "AspNetUserId");
        }
    }
}
