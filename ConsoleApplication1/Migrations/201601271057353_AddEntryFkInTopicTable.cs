namespace ConsoleApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEntryFkInTopicTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topic", "EntryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Topic", "EntryId");
            AddForeignKey("dbo.Topic", "EntryId", "dbo.Entry", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topic", "EntryId", "dbo.Entry");
            DropIndex("dbo.Topic", new[] { "EntryId" });
            DropColumn("dbo.Topic", "EntryId");
        }
    }
}
