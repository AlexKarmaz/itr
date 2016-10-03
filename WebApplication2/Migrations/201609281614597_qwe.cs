namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qwe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        PageId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SiteId = c.Int(),
                    })
                .PrimaryKey(t => t.PageId)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        SiteId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.SiteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pages", "SiteId", "dbo.Sites");
            DropIndex("dbo.Pages", new[] { "SiteId" });
            DropTable("dbo.Sites");
            DropTable("dbo.Pages");
        }
    }
}
