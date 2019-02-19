namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationships1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cards", "CardFaces_Id", "dbo.CardFaces");
            DropIndex("dbo.Cards", new[] { "CardFaces_Id" });
            AlterColumn("dbo.Cards", "CardFaces_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Cards", "CardFaces_Id");
            AddForeignKey("dbo.Cards", "CardFaces_Id", "dbo.CardFaces", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "CardFaces_Id", "dbo.CardFaces");
            DropIndex("dbo.Cards", new[] { "CardFaces_Id" });
            AlterColumn("dbo.Cards", "CardFaces_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Cards", "CardFaces_Id");
            AddForeignKey("dbo.Cards", "CardFaces_Id", "dbo.CardFaces", "Id", cascadeDelete: true);
        }
    }
}
