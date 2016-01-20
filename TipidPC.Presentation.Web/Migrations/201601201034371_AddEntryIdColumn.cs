namespace TipidPC.Presentation.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEntryIdColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "EntryId", c => c.Int(nullable: false));
            AddColumn("dbo.Topic", "EntryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Entry", "Message", c => c.String(nullable: false, maxLength: 2000));
            AlterColumn("dbo.Header", "Title", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Header", "Title", c => c.String());
            AlterColumn("dbo.Entry", "Message", c => c.String());
            DropColumn("dbo.Topic", "EntryId");
            DropColumn("dbo.Item", "EntryId");
        }
    }
}
