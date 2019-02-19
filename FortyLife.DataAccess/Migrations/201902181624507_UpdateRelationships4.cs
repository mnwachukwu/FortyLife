namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationships4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CardFaces", "Card_Id", "dbo.Cards");
            DropIndex("dbo.CardFaces", new[] { "Card_Id" });
            AlterColumn("dbo.CardFaces", "Card_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.CardFaces", "Card_Id");
            AddForeignKey("dbo.CardFaces", "Card_Id", "dbo.Cards", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardFaces", "Card_Id", "dbo.Cards");
            DropIndex("dbo.CardFaces", new[] { "Card_Id" });
            AlterColumn("dbo.CardFaces", "Card_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.CardFaces", "Card_Id");
            AddForeignKey("dbo.CardFaces", "Card_Id", "dbo.Cards", "Id", cascadeDelete: true);
        }
    }
}
