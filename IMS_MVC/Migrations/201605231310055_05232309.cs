namespace IMS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05232309 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IntInfoes", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "UserName", c => c.String());
            AddColumn("dbo.Users", "UserType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserType");
            DropColumn("dbo.Users", "UserName");
            DropColumn("dbo.IntInfoes", "UserId");
        }
    }
}
