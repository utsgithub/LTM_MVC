namespace IMS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Descriptive = c.String(),
                        DistrictId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistrictName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IntInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IntTypeId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        SetLabour = c.Int(nullable: false),
                        SetCost = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IntDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Comments = c.String(),
                        Reamaining = c.Int(nullable: false),
                        VisitDate = c.DateTime(nullable: false),
                        ApprovedByUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IntTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Labour = c.Int(nullable: false),
                        Cost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistrictId = c.Int(nullable: false),
                        MaxHours = c.Int(nullable: false),
                        MaxCost = c.Int(nullable: false),
                        UserType = c.Int(nullable: false),
                        AspNetUserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.IntTypes");
            DropTable("dbo.IntInfoes");
            DropTable("dbo.Districts");
            DropTable("dbo.Clients");
        }
    }
}
