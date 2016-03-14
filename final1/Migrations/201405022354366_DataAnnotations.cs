namespace final1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Images", "Name", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Images", "Annotation", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Images", "Annotation", c => c.String());
            AlterColumn("dbo.Images", "Name", c => c.String());
        }
    }
}
