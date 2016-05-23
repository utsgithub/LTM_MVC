namespace IMS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05240414 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IntInfoes", "IntDate", c => c.DateTime());
            AlterColumn("dbo.IntInfoes", "Reamaining", c => c.Int());
            AlterColumn("dbo.IntInfoes", "VisitDate", c => c.DateTime());
            AlterColumn("dbo.IntInfoes", "ApprovedByUserId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IntInfoes", "ApprovedByUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.IntInfoes", "VisitDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.IntInfoes", "Reamaining", c => c.Int(nullable: false));
            AlterColumn("dbo.IntInfoes", "IntDate", c => c.DateTime(nullable: false));
        }
    }
}
