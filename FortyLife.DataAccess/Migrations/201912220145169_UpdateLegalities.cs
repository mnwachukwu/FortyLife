namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLegalities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "Legalities_Historic", c => c.String());
            AddColumn("dbo.Cards", "Legalities_Pioneer", c => c.String());
            AddColumn("dbo.Cards", "Legalities_Brawl", c => c.String());
            DropColumn("dbo.Cards", "Legalities_Frontier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cards", "Legalities_Frontier", c => c.String());
            DropColumn("dbo.Cards", "Legalities_Brawl");
            DropColumn("dbo.Cards", "Legalities_Pioneer");
            DropColumn("dbo.Cards", "Legalities_Historic");
        }
    }
}
