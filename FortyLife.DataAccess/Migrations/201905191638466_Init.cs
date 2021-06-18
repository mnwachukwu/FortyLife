namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        ActivationKey = c.String(),
                        PasswordHash = c.String(),
                        PasswordSalt = c.String(),
                        DisplayName = c.String(),
                        AboutMe = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        CollectionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        TcgMidValue = c.Double(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastEditDate = c.DateTime(nullable: false),
                        AllowSuggestions = c.Boolean(),
                        Views = c.Int(nullable: false),
                        ApplicationUserId = c.Int(nullable: false),
                        AverageCmc = c.Double(),
                        Format = c.Int(),
                        Legality = c.Boolean(),
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
                    })
                .PrimaryKey(t => t.CollectionId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.CollectionCards",
                c => new
                    {
                        CollectionId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        SetCode = c.String(nullable: false, maxLength: 128),
                        Foil = c.Boolean(nullable: false),
                        Commander = c.Boolean(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CollectionId, t.Name, t.SetCode, t.Foil })
                .ForeignKey("dbo.Collections", t => t.CollectionId, cascadeDelete: true)
                .Index(t => t.CollectionId);
            
            CreateTable(
                "dbo.Suggestions",
                c => new
                    {
                        SuggestionId = c.Int(nullable: false, identity: true),
                        SuggesterId = c.Int(nullable: false),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        Collection_CollectionId = c.Int(),
                    })
                .PrimaryKey(t => t.SuggestionId)
                .ForeignKey("dbo.Collections", t => t.Collection_CollectionId)
                .Index(t => t.Collection_CollectionId);
            
            CreateTable(
                "dbo.CardFaces",
                c => new
                    {
                        CardFaceId = c.Int(nullable: false, identity: true),
                        Object = c.String(),
                        Name = c.String(),
                        ManaCost = c.String(),
                        TypeLine = c.String(),
                        OracleText = c.String(),
                        ColorsString = c.String(),
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
                .PrimaryKey(t => t.CardFaceId)
                .ForeignKey("dbo.Cards", t => t.Card_Id)
                .Index(t => t.Card_Id);
            
            CreateTable(
                "dbo.CardProductIds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardName = c.String(),
                        SetName = c.String(),
                        ProductId = c.Int(nullable: false),
                        CacheDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OracleId = c.String(),
                        MultiverseIdsString = c.String(),
                        MtgoId = c.Int(nullable: false),
                        MtgoFoilId = c.Int(nullable: false),
                        TcgPlayerId = c.Int(nullable: false),
                        Lang = c.String(),
                        ReleasedAt = c.DateTime(nullable: false),
                        Uri = c.String(),
                        ScryfallUri = c.String(),
                        Layout = c.String(),
                        HiResImage = c.Boolean(nullable: false),
                        Cmc = c.Double(nullable: false),
                        ColorIdentityString = c.String(),
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
                        GamesString = c.String(),
                        Reserved = c.Boolean(nullable: false),
                        Foil = c.Boolean(nullable: false),
                        NonFoil = c.Boolean(nullable: false),
                        Oversized = c.Boolean(nullable: false),
                        Promo = c.Boolean(nullable: false),
                        Reprint = c.Boolean(nullable: false),
                        Set = c.String(),
                        SetName = c.String(),
                        SetUri = c.String(),
                        SetSearchUri = c.String(),
                        ScryfallSetUri = c.String(),
                        RulingsUri = c.String(),
                        PrintsSearchUri = c.String(),
                        CollectorNumber = c.String(),
                        Digital = c.Boolean(nullable: false),
                        Rarity = c.String(),
                        Watermark = c.String(),
                        BorderColor = c.String(),
                        Frame = c.String(),
                        FrameEffect = c.String(),
                        FullArt = c.Boolean(nullable: false),
                        StorySpotlight = c.Boolean(nullable: false),
                        EdhrecRank = c.Int(nullable: false),
                        Usd = c.String(),
                        Eur = c.String(),
                        Tix = c.String(),
                        RelatedUris_Gatherer = c.String(),
                        RelatedUris_TcgPlayerDecks = c.String(),
                        RelatedUris_EdhRec = c.String(),
                        RelatedUris_MtgTop8 = c.String(),
                        PurchaseUris_TcgPlayer = c.String(),
                        PurchaseUris_CardMarket = c.String(),
                        PurchaseUris_CardHoarder = c.String(),
                        Object = c.String(),
                        Name = c.String(),
                        ManaCost = c.String(),
                        TypeLine = c.String(),
                        OracleText = c.String(),
                        ColorsString = c.String(),
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        SubTypeName = c.String(nullable: false, maxLength: 128),
                        LowPrice = c.Double(),
                        MidPrice = c.Double(),
                        HighPrice = c.Double(),
                        MarketPrice = c.Double(),
                        DirectLowPrice = c.Double(),
                        CacheDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.SubTypeName });
            
            CreateTable(
                "dbo.ProductDetails",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        Name = c.String(),
                        CleanName = c.String(),
                        ImageUrl = c.String(),
                        CategoryId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        Url = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        CacheDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.Rulings",
                c => new
                    {
                        OracleId = c.String(nullable: false, maxLength: 128),
                        RulingsUri = c.String(),
                        Object = c.String(),
                        Source = c.String(),
                        PublishedAt = c.DateTime(),
                        Comment = c.String(),
                        CacheDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OracleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardFaces", "Card_Id", "dbo.Cards");
            DropForeignKey("dbo.Collections", "ApplicationUserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Suggestions", "Collection_CollectionId", "dbo.Collections");
            DropForeignKey("dbo.CollectionCards", "CollectionId", "dbo.Collections");
            DropIndex("dbo.CardFaces", new[] { "Card_Id" });
            DropIndex("dbo.Suggestions", new[] { "Collection_CollectionId" });
            DropIndex("dbo.CollectionCards", new[] { "CollectionId" });
            DropIndex("dbo.Collections", new[] { "ApplicationUserId" });
            DropTable("dbo.Rulings");
            DropTable("dbo.ProductDetails");
            DropTable("dbo.Prices");
            DropTable("dbo.Cards");
            DropTable("dbo.CardProductIds");
            DropTable("dbo.CardFaces");
            DropTable("dbo.Suggestions");
            DropTable("dbo.CollectionCards");
            DropTable("dbo.Collections");
            DropTable("dbo.ApplicationUsers");
        }
    }
}
