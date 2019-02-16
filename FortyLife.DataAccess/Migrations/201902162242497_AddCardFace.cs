namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCardFace : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Cards", new[] { "Card_Id" });
            CreateTable(
                "dbo.CardFaces",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Object = c.String(),
                        Name = c.String(),
                        ManaCost = c.String(),
                        TypeLine = c.String(),
                        OracleText = c.String(),
                        Power = c.String(),
                        Toughness = c.String(),
                        FlavorText = c.String(),
                        Artist = c.String(),
                        IllustrationId = c.String(),
                        ImageUris_Small = c.String(),
                        ImageUris_Normal = c.String(),
                        ImageUris_Large = c.String(),
                        ImageUris_Png = c.String(),
                        ImageUris_ArtCrop = c.String(),
                        ImageUris_BorderCrop = c.String(),
                        Loyalty = c.String(),
                        CacheDate = c.DateTime(nullable: false),
                        Card_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Card_Id);
            
            AlterColumn("dbo.Cards", "MtgoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Cards", "MtgoFoilId", c => c.Int(nullable: false));
            AlterColumn("dbo.Cards", "TcgPlayerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Cards", "ReleasedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Cards", "HiResImage", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Cards", "Cmc", c => c.Double(nullable: false));
            AlterColumn("dbo.Cards", "Reserved", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Cards", "Foil", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Cards", "NonFoil", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Cards", "Oversized", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Cards", "Promo", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Cards", "Reprint", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Cards", "Digital", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Cards", "FullArt", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Cards", "StorySpotlight", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Cards", "EdhrecRank", c => c.Int(nullable: false));
            DropColumn("dbo.Cards", "Discriminator");
            DropColumn("dbo.Cards", "Card_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cards", "Card_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Cards", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropIndex("dbo.CardFaces", new[] { "Card_Id" });
            AlterColumn("dbo.Cards", "EdhrecRank", c => c.Int());
            AlterColumn("dbo.Cards", "StorySpotlight", c => c.Boolean());
            AlterColumn("dbo.Cards", "FullArt", c => c.Boolean());
            AlterColumn("dbo.Cards", "Digital", c => c.Boolean());
            AlterColumn("dbo.Cards", "Reprint", c => c.Boolean());
            AlterColumn("dbo.Cards", "Promo", c => c.Boolean());
            AlterColumn("dbo.Cards", "Oversized", c => c.Boolean());
            AlterColumn("dbo.Cards", "NonFoil", c => c.Boolean());
            AlterColumn("dbo.Cards", "Foil", c => c.Boolean());
            AlterColumn("dbo.Cards", "Reserved", c => c.Boolean());
            AlterColumn("dbo.Cards", "Cmc", c => c.Double());
            AlterColumn("dbo.Cards", "HiResImage", c => c.Boolean());
            AlterColumn("dbo.Cards", "ReleasedAt", c => c.DateTime());
            AlterColumn("dbo.Cards", "TcgPlayerId", c => c.Int());
            AlterColumn("dbo.Cards", "MtgoFoilId", c => c.Int());
            AlterColumn("dbo.Cards", "MtgoId", c => c.Int());
            DropTable("dbo.CardFaces");
            CreateIndex("dbo.Cards", "Card_Id");
        }
    }
}
