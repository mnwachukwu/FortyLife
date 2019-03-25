namespace FortyLife.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductDetails1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ProductDetails");
            AlterColumn("dbo.ProductDetails", "ProductId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ProductDetails", "ProductId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProductDetails");
            AlterColumn("dbo.ProductDetails", "ProductId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ProductDetails", "ProductId");
        }
    }
}
