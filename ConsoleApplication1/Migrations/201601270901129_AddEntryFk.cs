namespace ConsoleApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEntryFk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Item", "Entry_Id", "dbo.Entry");
            DropIndex("dbo.Item", new[] { "Entry_Id" });
            RenameColumn(table: "dbo.Item", name: "Entry_Id", newName: "EntryId");
            AlterColumn("dbo.Item", "EntryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Item", "EntryId");
            AddForeignKey("dbo.Item", "EntryId", "dbo.Entry", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Item", "EntryId", "dbo.Entry");
            DropIndex("dbo.Item", new[] { "EntryId" });
            AlterColumn("dbo.Item", "EntryId", c => c.Int());
            RenameColumn(table: "dbo.Item", name: "EntryId", newName: "Entry_Id");
            CreateIndex("dbo.Item", "Entry_Id");
            AddForeignKey("dbo.Item", "Entry_Id", "dbo.Entry", "Id");
        }
    }
}
