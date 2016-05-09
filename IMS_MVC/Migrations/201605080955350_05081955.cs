namespace IMS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05081955 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UserType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UserType", c => c.Int(nullable: false));
        }
    }
}
