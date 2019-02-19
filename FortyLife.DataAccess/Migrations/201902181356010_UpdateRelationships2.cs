namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateRelationships2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Cards", new[] { "CardFaces_Id" });
            AddColumn("dbo.CardFaces", "Card_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.CardFaces", "Card_Id");
            DropColumn("dbo.Cards", "CardFaces_Id");
        }

        public override void Down()
        {
            AddColumn("dbo.Cards", "CardFaces_Id", c => c.String(maxLength: 128));
            DropIndex("dbo.CardFaces", new[] { "Card_Id" });
            DropColumn("dbo.CardFaces", "Card_Id");
            CreateIndex("dbo.Cards", "CardFaces_Id");
        }
    }
}
