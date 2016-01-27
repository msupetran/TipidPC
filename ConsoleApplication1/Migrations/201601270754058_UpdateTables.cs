namespace ConsoleApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookmark",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HeaderID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Header", t => t.HeaderID, cascadeDelete: true)
                .Index(t => t.HeaderID);
            
            CreateTable(
                "dbo.Header",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        UserId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Entry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false, maxLength: 2000),
                        HeaderId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Header", t => t.HeaderId, cascadeDelete: true)
                .Index(t => t.HeaderId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HeaderId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Section = c.Int(nullable: false),
                        Condition = c.Int(nullable: false),
                        Warranty = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Expiry = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Header", t => t.HeaderId, cascadeDelete: true)
                .Index(t => t.HeaderId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        IsPositive = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Section",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Topic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HeaderId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Header", t => t.HeaderId, cascadeDelete: true)
                .ForeignKey("dbo.Section", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.HeaderId)
                .Index(t => t.SectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topic", "SectionId", "dbo.Section");
            DropForeignKey("dbo.Topic", "HeaderId", "dbo.Header");
            DropForeignKey("dbo.Item", "HeaderId", "dbo.Header");
            DropForeignKey("dbo.Item", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Bookmark", "HeaderID", "dbo.Header");
            DropForeignKey("dbo.Entry", "HeaderId", "dbo.Header");
            DropIndex("dbo.Topic", new[] { "SectionId" });
            DropIndex("dbo.Topic", new[] { "HeaderId" });
            DropIndex("dbo.Item", new[] { "CategoryId" });
            DropIndex("dbo.Item", new[] { "HeaderId" });
            DropIndex("dbo.Entry", new[] { "HeaderId" });
            DropIndex("dbo.Bookmark", new[] { "HeaderID" });
            DropTable("dbo.Topic");
            DropTable("dbo.Section");
            DropTable("dbo.Rating");
            DropTable("dbo.Location");
            DropTable("dbo.Item");
            DropTable("dbo.Category");
            DropTable("dbo.Entry");
            DropTable("dbo.Header");
            DropTable("dbo.Bookmark");
        }
    }
}
