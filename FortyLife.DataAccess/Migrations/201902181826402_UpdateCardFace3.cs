namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCardFace3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CardFaces", "Colors", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CardFaces", "Colors");
        }
    }
}
