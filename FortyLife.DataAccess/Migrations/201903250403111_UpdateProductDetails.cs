namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductDetails : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ProductDetails");
            AlterColumn("dbo.ProductDetails", "ProductId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ProductDetails", "ProductId");
            DropColumn("dbo.ProductDetails", "ProductDetailId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductDetails", "ProductDetailId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.ProductDetails");
            AlterColumn("dbo.ProductDetails", "ProductId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ProductDetails", "ProductDetailId");
        }
    }
}
