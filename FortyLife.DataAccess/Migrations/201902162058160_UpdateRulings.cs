namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRulings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rulings", "RulingsUri", c => c.String());
            DropColumn("dbo.Rulings", "RulingUri");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rulings", "RulingUri", c => c.String());
            DropColumn("dbo.Rulings", "RulingsUri");
        }
    }
}
