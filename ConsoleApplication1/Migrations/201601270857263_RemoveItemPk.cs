namespace ConsoleApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveItemPk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Entry", "Item_Id", "dbo.Item");
            DropIndex("dbo.Entry", new[] { "Item_Id" });
            AddColumn("dbo.Item", "Entry_Id", c => c.Int());
            CreateIndex("dbo.Item", "Entry_Id");
            AddForeignKey("dbo.Item", "Entry_Id", "dbo.Entry", "Id");
            DropColumn("dbo.Entry", "Item_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Entry", "Item_Id", c => c.Int());
            DropForeignKey("dbo.Item", "Entry_Id", "dbo.Entry");
            DropIndex("dbo.Item", new[] { "Entry_Id" });
            DropColumn("dbo.Item", "Entry_Id");
            CreateIndex("dbo.Entry", "Item_Id");
            AddForeignKey("dbo.Entry", "Item_Id", "dbo.Item", "Id");
        }
    }
}
