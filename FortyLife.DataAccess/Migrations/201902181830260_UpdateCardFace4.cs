namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCardFace4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CardFaces", "Colors");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CardFaces", "Colors", c => c.String());
        }
    }
}
