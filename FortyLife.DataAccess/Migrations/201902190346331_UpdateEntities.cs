namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "MultiverseIdsString", c => c.String());
            AddColumn("dbo.Cards", "ColorIdentityString", c => c.String());
            AddColumn("dbo.Cards", "GamesString", c => c.String());
            AddColumn("dbo.Cards", "ColorsString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "ColorsString");
            DropColumn("dbo.Cards", "GamesString");
            DropColumn("dbo.Cards", "ColorIdentityString");
            DropColumn("dbo.Cards", "MultiverseIdsString");
        }
    }
}
