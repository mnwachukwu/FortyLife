namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        PasswordSalt = c.String(),
                        DisplayName = c.String(),
                        AboutMe = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        CollectionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TcgMidValue = c.Double(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastEditDate = c.DateTime(nullable: false),
                        Views = c.Int(nullable: false),
                        AverageCmc = c.Double(),
                        Format = c.Int(),
                        Legality = c.Boolean(),
                        AllowSuggestions = c.Boolean(),
                        CardTypeStatistics_CreaturePct = c.Double(),
                        CardTypeStatistics_ArtifactPct = c.Double(),
                        CardTypeStatistics_EnchantmentPct = c.Double(),
                        CardTypeStatistics_InstantPct = c.Double(),
                        CardTypeStatistics_SorceryPct = c.Double(),
                        CardTypeStatistics_PlaneswalkerPct = c.Double(),
                        CardTypeStatistics_LandPct = c.Double(),
                        ColorStatistics_WhitePct = c.Double(),
                        ColorStatistics_BluePct = c.Double(),
                        ColorStatistics_BlackPct = c.Double(),
                        ColorStatistics_RedPct = c.Double(),
                        ColorStatistics_GreenPct = c.Double(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.CollectionId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.CollectionCards",
                c => new
                    {
                        CollectionCardId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SetCode = c.String(),
                        Foil = c.Boolean(nullable: false),
                        Collection_CollectionId = c.Int(),
                    })
                .PrimaryKey(t => t.CollectionCardId)
                .ForeignKey("dbo.Collections", t => t.Collection_CollectionId)
                .Index(t => t.Collection_CollectionId);
            
            CreateTable(
                "dbo.Suggestions",
                c => new
                    {
                        SuggestionId = c.Int(nullable: false, identity: true),
                        SuggesterName = c.String(),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        Deck_CollectionId = c.Int(),
                    })
                .PrimaryKey(t => t.SuggestionId)
                .ForeignKey("dbo.Collections", t => t.Deck_CollectionId)
                .Index(t => t.Deck_CollectionId);
            
            CreateTable(
                "dbo.Card",
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
                        OracleId = c.String(),
                        MtgoId = c.Int(),
                        MtgoFoilId = c.Int(),
                        TcgPlayerId = c.Int(),
                        Lang = c.String(),
                        ReleasedAt = c.DateTime(),
                        Uri = c.String(),
                        ScryfallUri = c.String(),
                        Layout = c.String(),
                        HiResImage = c.Boolean(),
                        Cmc = c.Double(),
                        Reserved = c.Boolean(),
                        Foil = c.Boolean(),
                        NonFoil = c.Boolean(),
                        Oversized = c.Boolean(),
                        Promo = c.Boolean(),
                        Reprint = c.Boolean(),
                        Set = c.String(),
                        SetName = c.String(),
                        SetUri = c.String(),
                        SetSearchUri = c.String(),
                        ScryfallSetUri = c.String(),
                        RulingsUri = c.String(),
                        PrintsSearchUri = c.String(),
                        CollectorNumber = c.String(),
                        Digital = c.Boolean(),
                        Rarity = c.String(),
                        Watermark = c.String(),
                        BorderColor = c.String(),
                        Frame = c.String(),
                        FrameEffect = c.String(),
                        FullArt = c.Boolean(),
                        StorySpotlight = c.Boolean(),
                        EdhrecRank = c.Int(),
                        Usd = c.String(),
                        Eur = c.String(),
                        Tix = c.String(),
                        Legalities_Standard = c.String(),
                        Legalities_Future = c.String(),
                        Legalities_Frontier = c.String(),
                        Legalities_Modern = c.String(),
                        Legalities_Legacy = c.String(),
                        Legalities_Pauper = c.String(),
                        Legalities_Vintage = c.String(),
                        Legalities_Penny = c.String(),
                        Legalities_Commander = c.String(),
                        Legalities_Duel = c.String(),
                        Legalities_OldSchool = c.String(),
                        PurchaseUris_TcgPlayer = c.String(),
                        PurchaseUris_CardMarket = c.String(),
                        PurchaseUris_CardHoarder = c.String(),
                        RelatedUris_Gatherer = c.String(),
                        RelatedUris_TcgPlayerDecks = c.String(),
                        RelatedUris_EdhRec = c.String(),
                        RelatedUris_MtgTop8 = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Card_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Card", t => t.Card_Id)
                .Index(t => t.Card_Id);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        PriceId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        LowPrice = c.Double(),
                        MidPrice = c.Double(),
                        HighPrice = c.Double(),
                        MarketPrice = c.Double(),
                        DirectLowPrice = c.Double(),
                        SubTypeName = c.String(),
                    })
                .PrimaryKey(t => t.PriceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Card", "Card_Id", "dbo.Card");
            DropForeignKey("dbo.Collections", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Suggestions", "Deck_CollectionId", "dbo.Collections");
            DropForeignKey("dbo.CollectionCards", "Collection_CollectionId", "dbo.Collections");
            DropIndex("dbo.Card", new[] { "Card_Id" });
            DropIndex("dbo.Suggestions", new[] { "Deck_CollectionId" });
            DropIndex("dbo.CollectionCards", new[] { "Collection_CollectionId" });
            DropIndex("dbo.Collections", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Prices");
            DropTable("dbo.Card");
            DropTable("dbo.Suggestions");
            DropTable("dbo.CollectionCards");
            DropTable("dbo.Collections");
            DropTable("dbo.ApplicationUsers");
        }
    }
}
