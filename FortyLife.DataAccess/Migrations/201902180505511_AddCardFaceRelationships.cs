namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCardFaceRelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CardFaces", "Card_Id", "dbo.Cards");
            DropIndex("dbo.CardFaces", new[] { "Card_Id" });
            AddColumn("dbo.Cards", "CardFacesId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Cards", "CardFacesId");
            AddForeignKey("dbo.Cards", "CardFacesId", "dbo.CardFaces", "Id", cascadeDelete: true);
            DropColumn("dbo.CardFaces", "Card_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CardFaces", "Card_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Cards", "CardFacesId", "dbo.CardFaces");
            DropIndex("dbo.Cards", new[] { "CardFacesId" });
            DropColumn("dbo.Cards", "CardFacesId");
            CreateIndex("dbo.CardFaces", "Card_Id");
            AddForeignKey("dbo.CardFaces", "Card_Id", "dbo.Cards", "Id");
        }
    }
}
