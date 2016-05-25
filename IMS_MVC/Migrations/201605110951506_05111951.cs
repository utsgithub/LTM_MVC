namespace IMS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05111951 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.IntInfoes", "IntTypeId");
            CreateIndex("dbo.IntInfoes", "ClientId");
            AddForeignKey("dbo.IntInfoes", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IntInfoes", "IntTypeId", "dbo.IntTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IntInfoes", "IntTypeId", "dbo.IntTypes");
            DropForeignKey("dbo.IntInfoes", "ClientId", "dbo.Clients");
            DropIndex("dbo.IntInfoes", new[] { "ClientId" });
            DropIndex("dbo.IntInfoes", new[] { "IntTypeId" });
        }
    }
}
