namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sites", "MenuId", "dbo.Menus");
            DropIndex("dbo.Sites", new[] { "MenuId" });
            AlterColumn("dbo.Sites", "MenuId", c => c.Int());
            CreateIndex("dbo.Sites", "MenuId");
            AddForeignKey("dbo.Sites", "MenuId", "dbo.Menus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sites", "MenuId", "dbo.Menus");
            DropIndex("dbo.Sites", new[] { "MenuId" });
            AlterColumn("dbo.Sites", "MenuId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sites", "MenuId");
            AddForeignKey("dbo.Sites", "MenuId", "dbo.Menus", "Id", cascadeDelete: true);
        }
    }
}
