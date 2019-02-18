namespace FortyLife.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddLocalDataStore : DbMigration
    {
        public override void Up()
        {
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
                "dbo.ProductDetails",
                c => new
                    {
                        ProductDetailId = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.ProductDetailId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductDetails");
            DropTable("dbo.CardProductIds");
        }
    }
}
