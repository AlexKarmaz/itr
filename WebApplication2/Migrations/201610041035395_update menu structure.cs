namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemenustructure : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuItems", "Menu_Id1", "dbo.Menus");
            DropForeignKey("dbo.Sites", "MenuId", "dbo.Menus");
            DropIndex("dbo.Sites", new[] { "MenuId" });
            DropIndex("dbo.MenuItems", new[] { "Menu_Id1" });
            AddColumn("dbo.Menus", "IsTopBar", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Sites", "MenuId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sites", "MenuId");
            AddForeignKey("dbo.Sites", "MenuId", "dbo.Menus", "Id", cascadeDelete: true);
            DropColumn("dbo.Menus", "IsTopBarExicist");
            DropColumn("dbo.Menus", "IsSideBarExicist");
            DropColumn("dbo.MenuItems", "Menu_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MenuItems", "Menu_Id1", c => c.Int());
            AddColumn("dbo.Menus", "IsSideBarExicist", c => c.Boolean(nullable: false));
            AddColumn("dbo.Menus", "IsTopBarExicist", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Sites", "MenuId", "dbo.Menus");
            DropIndex("dbo.Sites", new[] { "MenuId" });
            AlterColumn("dbo.Sites", "MenuId", c => c.Int());
            DropColumn("dbo.Menus", "IsTopBar");
            CreateIndex("dbo.MenuItems", "Menu_Id1");
            CreateIndex("dbo.Sites", "MenuId");
            AddForeignKey("dbo.Sites", "MenuId", "dbo.Menus", "Id");
            AddForeignKey("dbo.MenuItems", "Menu_Id1", "dbo.Menus", "Id");
        }
    }
}
