namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pageupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "HtmlCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "HtmlCode");
        }
    }
}
