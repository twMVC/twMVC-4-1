namespace Mvc4DatabaseMigrationApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Name");
        }
    }
}
