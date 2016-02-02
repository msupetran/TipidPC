namespace ConsoleApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDurationColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "Duration");
        }
    }
}
