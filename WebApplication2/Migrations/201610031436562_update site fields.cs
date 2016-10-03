namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesitefields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "TemplateId", c => c.Int(nullable: false));
            AddColumn("dbo.Sites", "MenuId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "MenuId");
            DropColumn("dbo.Sites", "TemplateId");
        }
    }
}
