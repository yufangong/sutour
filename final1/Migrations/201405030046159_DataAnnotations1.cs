namespace final1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CityImages", "Name", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.CityImages", "Annotation", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Landmarks", "LandmarkName", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Landmarks", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Landmarks", "Direction", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Landmarks", "Direction", c => c.String());
            AlterColumn("dbo.Landmarks", "Description", c => c.String());
            AlterColumn("dbo.Landmarks", "LandmarkName", c => c.String());
            AlterColumn("dbo.CityImages", "Annotation", c => c.String());
            AlterColumn("dbo.CityImages", "Name", c => c.String());
        }
    }
}
