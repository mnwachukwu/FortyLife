namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDbCaches : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CardProductIds");
            DropTable("dbo.Prices");
            DropTable("dbo.ProductDetails");
            DropTable("dbo.Rulings");
        }
        
        public override void Down()
        {
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
            
        }
    }
}
