namespace IMS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05232311 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.IntInfoes", "UserId");
            AddForeignKey("dbo.IntInfoes", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IntInfoes", "UserId", "dbo.Users");
            DropIndex("dbo.IntInfoes", new[] { "UserId" });
        }
    }
}
