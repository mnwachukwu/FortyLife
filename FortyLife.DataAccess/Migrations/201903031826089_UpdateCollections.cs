namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCollections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Collections", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Collections", "Description");
        }
    }
}
