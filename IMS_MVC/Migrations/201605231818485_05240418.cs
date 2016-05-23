namespace IMS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05240418 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IntInfoes", "SetLabour", c => c.Int());
            AlterColumn("dbo.IntInfoes", "SetCost", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IntInfoes", "SetCost", c => c.Int(nullable: false));
            AlterColumn("dbo.IntInfoes", "SetLabour", c => c.Int(nullable: false));
        }
    }
}
