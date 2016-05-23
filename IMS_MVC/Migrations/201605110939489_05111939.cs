namespace IMS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05111939 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Clients", "DistrictId");
            AddForeignKey("dbo.Clients", "DistrictId", "dbo.Districts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "DistrictId", "dbo.Districts");
            DropIndex("dbo.Clients", new[] { "DistrictId" });
        }
    }
}
